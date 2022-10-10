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

namespace OpenHumanTask.Runtime.Application
{
    /// <summary>
    /// Represents an exception thrown whenever an operation returned a non-success <see cref="IOperationResult"/>
    /// </summary>
    public class OperationException
        : Exception
    {

        /// <summary>
        /// Initializes a new <see cref="OperationException"/>
        /// </summary>
        /// <param name="result">The <see cref="IOperationResult"/> which is the cause of the <see cref="OperationException"/></param>
        public OperationException(IOperationResult result)
            : base($"The operation was executed with a non-success result code '{result.Code}'.{Environment.NewLine}Errors: {(result.Errors == null ? "" : string.Join(Environment.NewLine, result.Errors.Select(e => $"{e.Code}: {e.Message}")))}")
        {

        }

    }

}
