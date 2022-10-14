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

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OpenHumanTask.Runtime.Integration.Models
{
    /// <summary>
    /// Represents an object used to describe a plugin
    /// </summary>
    public class PluginMetadata
    {

        /// <summary>
        /// Gets the plugin's type
        /// </summary>
        [JsonPropertyName("type")]
        [Newtonsoft.Json.JsonProperty("type")]
        public virtual PluginType Type { get; set; } = PluginType.Generic;

        /// <summary>
        /// Gets the plugin's name
        /// </summary>
        [Required, MinLength(1)]
        [JsonPropertyName("name")]
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public virtual string Name { get; set; } = null!;

        /// <summary>
        /// Gets the plugin's description
        /// </summary>
        [JsonPropertyName("description")]
        [Newtonsoft.Json.JsonProperty("description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets the plugin's version
        /// </summary>
        [JsonPropertyName("version")]
        [Newtonsoft.Json.JsonProperty("version")]
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets the plugin's authors
        /// </summary>
        [JsonPropertyName("authors")]
        [Newtonsoft.Json.JsonProperty("authors")]
        public virtual string Authors { get; set; }

        /// <summary>
        /// Gets the plugin's copyright
        /// </summary>
        [JsonPropertyName("copyright")]
        [Newtonsoft.Json.JsonProperty("copyright")]
        public virtual string Copyright { get; set; }

        /// <summary>
        /// Gets a <see cref="List{T}"/> containing the plugin's tags
        /// </summary>
        [JsonPropertyName("tags")]
        [Newtonsoft.Json.JsonProperty("tags")]
        public virtual List<string> Tags { get; set; } = new();

        /// <summary>
        /// Gets the plugin's license file <see cref="Uri"/>
        /// </summary>
        [JsonPropertyName("licenseUri")]
        [Newtonsoft.Json.JsonProperty("licenseUri")]
        public virtual Uri LicenseUri { get; set; }

        /// <summary>
        /// Gets the plugin's readme file <see cref="Uri"/>
        /// </summary>
        [JsonPropertyName("readmeUri")]
        [Newtonsoft.Json.JsonProperty("readmeUri")]
        public virtual Uri ReadmeUri { get; set; }

        /// <summary>
        /// Gets the plugin's website <see cref="Uri"/>
        /// </summary>
        [JsonPropertyName("websiteUri")]
        [Newtonsoft.Json.JsonProperty("websiteUri")]
        public virtual Uri WebsiteUri { get; set; }

        /// <summary>
        /// Gets the plugin's repository <see cref="Uri"/>
        /// </summary>
        [JsonPropertyName("repositoryUri")]
        [Newtonsoft.Json.JsonProperty("repositoryUri")]
        public virtual Uri RepositoryUri { get; set; }

        /// <summary>
        /// Gets the plugin's <see cref="Assembly"/> file name
        /// </summary>
        [Required, MinLength(1)]
        [JsonPropertyName("assemblyFile")]
        [Newtonsoft.Json.JsonProperty("assemblyFile", Required = Newtonsoft.Json.Required.Always)]
        public virtual string AssemblyFileName { get; set; } = null!;

        /// <inheritdoc/>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(this.Version))
                return this.Name;
            else
                return $"{this.Name}:{this.Version}";
        }

    }

}
