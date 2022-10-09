// Copyright © 2022-Present The Open Human Task Specification Authors. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.ObjectModel;

namespace OpenHumanTask.Runtime
{

    /// <summary>
    /// Defines extensions for <see cref="IDictionary{TKey, TValue}"/> implementations
    /// </summary>
    public static class IDictionaryExtensions
    {

        /// <summary>
        /// Converts the <see cref="IDictionary{TKey, TValue}"/> to a new <see cref="IReadOnlyDictionary{TKey, TValue}"/>
        /// </summary>
        /// <typeparam name="TKey">The type of key used by the <see cref="IDictionary{TKey, TValue}"/> to convert</typeparam>
        /// <typeparam name="TValue">The type of value used by the <see cref="IDictionary{TKey, TValue}"/> to convert</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> to convert</param>
        /// <returns>A new <see cref="IReadOnlyDictionary{TKey, TValue}"/></returns>
        public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }

    }

}
