/*
 * Copyright © 2022-Present The Synapse Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using Neuroglia.Data.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenHumanTask.Runtime.Application.Services;
using System.Dynamic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace OpenHumanTask.Runtime.Application
{

    /// <summary>
    /// Defines extensions for <see cref="IExpressionEvaluator"/>s
    /// </summary>
    public static class IExpressionEvaluatorExtensions
    {

        /// <summary>
        /// Evaluates the specified runtime expression
        /// </summary>
        /// <param name="evaluator">The extended <see cref="IExpressionEvaluator"/></param>
        /// <param name="expression">The runtime expression to evaluate</param>
        /// <param name="input">The input to evaluate th expression against</param>
        /// <param name="context">The current <see cref="HumanTaskRuntimeContext"/></param>
        /// <returns>The evaluation result</returns>
        public static object? Evaluate(this IExpressionEvaluator evaluator, string expression, object input, HumanTaskRuntimeContext context)
        {
            if (evaluator == null) throw new ArgumentNullException(nameof(evaluator));
            if (string.IsNullOrWhiteSpace(expression)) throw new ArgumentNullException(nameof(expression));
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (context == null) throw new ArgumentNullException(nameof(context));
            return evaluator.Evaluate(expression, input, BuildArguments(context));
        }

        /// <summary>
        /// Evaluates the specified runtime expression
        /// </summary>
        /// <typeparam name="T">The expected type of the evaluation's result</typeparam>
        /// <param name="evaluator">The extended <see cref="IExpressionEvaluator"/></param>
        /// <param name="expression">The runtime expression to evaluate</param>
        /// <param name="input">The input to evaluate th expression against</param>
        /// <param name="context">The current <see cref="HumanTaskRuntimeContext"/></param>
        /// <returns>The evaluation result</returns>
        public static T? Evaluate<T>(this IExpressionEvaluator evaluator, string expression, object input, HumanTaskRuntimeContext context)
        {
            if (evaluator == null) throw new ArgumentNullException(nameof(evaluator));
            if (string.IsNullOrWhiteSpace(expression)) throw new ArgumentNullException(nameof(expression));
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (context == null) throw new ArgumentNullException(nameof(context));
            return evaluator.Evaluate<T>(expression, input, BuildArguments(context));
        }

        /// <summary>
        /// Evaluates the specified runtime expression
        /// </summary>
        /// <param name="evaluator">The extended <see cref="IExpressionEvaluator"/></param>
        /// <param name="expressionObject">The runtime expression to evaluate</param>
        /// <param name="input">The input to evaluate th expression against</param>
        /// <param name="context">The current <see cref="HumanTaskRuntimeContext"/></param>
        /// <returns>The evaluation result</returns>
        public static object? Evaluate(this IExpressionEvaluator evaluator, object expressionObject, object input, HumanTaskRuntimeContext context)
        {
            if (evaluator == null) throw new ArgumentNullException(nameof(evaluator));
            if (expressionObject == null) throw new ArgumentNullException(nameof(expressionObject));
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (expressionObject is string expression) return Evaluate(evaluator, expression, input, context);
            var json = JsonConvert.SerializeObject(expressionObject);
            foreach (Match match in Regex.Matches(json, @"""\$\{.+?\}"""))
            {
                expression = match.Value[3..^2].Trim().Replace(@"\""", @"""");
                var evaluationResult = evaluator.Evaluate(expression, input, context);
                if (evaluationResult == null) continue;
                var valueToken = JToken.FromObject(evaluationResult);
                var value = null as string;
                if (valueToken != null)
                {
                    value = valueToken.Type switch
                    {
                        JTokenType.String => @$"""{valueToken}""",
                        _ => valueToken.ToString(),
                    };
                }
                if (string.IsNullOrEmpty(value))
                    value = "null";
                json = json.Replace(match.Value, value);
            }
            return JsonConvert.DeserializeObject<ExpandoObject>(json);
        }

        /// <summary>
        /// Evaluates the specified localized string object
        /// </summary>
        /// <param name="evaluator">The extended <see cref="IExpressionEvaluator"/></param>
        /// <param name="expressionObject">The runtime expression to evaluate</param>
        /// <param name="input">The input to evaluate th expression against</param>
        /// <param name="context">The current <see cref="HumanTaskRuntimeContext"/></param>
        /// <returns>The evaluation result</returns>
        public static Dictionary<string, string> EvaluateLocalizedStrings(this IExpressionEvaluator evaluator, object expressionObject, object input, HumanTaskRuntimeContext context)
        {
            if (evaluator == null) throw new ArgumentNullException(nameof(evaluator));
            if (expressionObject == null) throw new ArgumentNullException(nameof(expressionObject));
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (expressionObject is string expression)
            {
                var invariantValue = string.Empty;
                if (expression.IsRuntimeExpression())
                    invariantValue = Evaluate<string>(evaluator, expression, input, context);
                else
                    invariantValue = expression;
                return new Dictionary<string, string>() { { CultureInfo.InvariantCulture.TwoLetterISOLanguageName, invariantValue! } };
            }
            else
            {
                var expressionDictionary = expressionObject.ToDictionary<string>();
                var results = new Dictionary<string, string>();
                foreach(var kvp in expressionDictionary)
                {
                    var key = kvp.Key;
                    if (key.IsRuntimeExpression()) key = evaluator.Evaluate<string>(key, input, context);
                    if(string.IsNullOrEmpty(key)) continue;
                    if (!key.IsValidLanguageCode()) continue;
                    var value = kvp.Value;
                    if(!string.IsNullOrWhiteSpace(value) && value.IsRuntimeExpression()) evaluator.Evaluate<string>(key, input, context);
                    results.Add(key.ToLowerInvariant(), value);
                }
                return results;
            }
        }

        private static IDictionary<string, object> BuildArguments(HumanTaskRuntimeContext context)
        {
            return new Dictionary<string, object>() 
            {
                { "CONTEXT", context }
            };
        }

    }

}
