﻿
${

    using System.Text;
    using System.Text.RegularExpressions;

    Template(Settings settings)
    {
        settings.PartialRenderingMode = PartialRenderingMode.Combined;
        settings.IncludeProject("OpenHumanTask.Runtime.Application");
        settings.OutputFilenameFactory = (file) => GetOutputFilename("command", file);
    }

    string GetOutputFilename(string templateType, File file)
    {
        Class c = file.Classes.First();
        if(c.Name.EndsWith("Command"))
        {
            System.IO.DirectoryInfo aggregateDirectory = new System.IO.FileInfo(file.FullName).Directory;
            System.IO.DirectoryInfo directory = GetSourcesDirectory(file);
            string directoryName = $"Commands\\{aggregateDirectory.Name}\\Generated";
            string relativePath = $"..\\{directoryName}";
            string absolutePath = System.IO.Path.Combine(directory.FullName, "core", $"OpenHumanTask.Runtime.Integration", directoryName);
            if(!System.IO.Directory.Exists(absolutePath))
                System.IO.Directory.CreateDirectory(absolutePath);
            return $"{relativePath}\\{GetCommandName(c)}.cs";
        }
        throw new InvalidOperationException($"Failed to resolve the output file name for file '{file.FullName}'");
    }

    System.IO.DirectoryInfo GetSourcesDirectory(File file)
    {
        System.IO.DirectoryInfo directory = new System.IO.FileInfo(file.FullName).Directory;
        while (directory.Name != "src" 
            && directory.Parent != null)
        {
            directory = directory.Parent;
        }
        return directory;
    }

    string GetEventName(Class c)
    {
        return c.Name.Replace("DomainEvent", "IntegrationEvent");
    }

    string GetCommandName(Class c)
    {
        return c.Name;
    }

    string GetAggregateName(Class c)
    {
        return c.BaseClass.TypeArguments.First().Name;
    }

    string GetPluralizedAggregateName(Class c)
    {
        if(c.Name == "AuthorizationPolicy")
            return "AuthorizationPolicies";
        return $"{GetAggregateName(c)}s";
    }

    string GetModelName(Class c)
    {
        return c.Name;
    }

    string GetClassName(Class c)
    {
        return GetCommandName(c);
    }

    string GetBaseClass(Class c) 
    {
        if(c.BaseClass.Name == "Command")
            return "Command";
        else
            return GetType(c.BaseClass);
        
    }

    string GetType(Type type)
    {
        var typeName = type.OriginalName switch
        {
            "Object" or "Object?" or "object" or "object?" => "object?",
            "ExpandoObject" or "ExpandoObject?" => "Dictionary<string, object>?",
            "IDictionary" or "IDictionary" or "IReadOnlyDictionary" or "IReadOnlyDictionary?" => "Dictionary",
            "ICollection" or "ICollection?" or "IReadOnlyCollection" or "IReadOnlyCollection?" or "IEnumerable" or "IEnumerable?" or "IList" or "IList?" or "IReadOnlyList" or "IReadOnlyList?" => "List",
            _ => type.OriginalName
        };
        if(typeName == "TKey") return "object";
        if(type.IsPrimitive && type.OriginalName != "string?" && !type.TypeArguments.Any())
            return typeName;
        if(typeName.EndsWith("?"))
            typeName = typeName.Substring(0, typeName.Length - 1);
        if(type.TypeArguments.Any() && !typeName.Contains("<"))
            typeName += $"<{string.Join(", ", type.TypeArguments.Select(a => GetType(a)))}>";
        if(typeName.EndsWith("[]"))
            typeName = typeName.Substring(0, typeName.Length - 2);
        if(type.OriginalName.EndsWith("[]"))
            typeName += "[]";
        return typeName;
    }

    string Indent(int amount, string text)
    {
        string indents = "";
        for(int index = 0; index < amount; index++)
        {
            indents += "\t";
        }
        return indents + text;
    }

    string SanitizeDocComments(string comments)
    {
        foreach(Match match in Regex.Matches(comments, @"<see cref=""[^\s]* \/>"))
        {
            string value = Regex.Match(match.Value, @"(?<=<see cref="")[^""]*").Value;
            value = value.Split('.').Last();
            var index = value.IndexOf("<");
            if(index > -1)
                value = value.Substring(0, index);
            comments = comments.Replace(match.Value, value);
        }
        foreach(Match match in Regex.Matches(comments, @"<see href="".*"">[^.]*<\/see>"))
        {
            Match innerMatch = Regex.Match(match.Value, @"(?<=<see href="")(?:[^""]*"">)(.*)(?=<\/see>)");
            string value = innerMatch.Groups[1].Value;
            var index = value.IndexOf("<");
            if(index > -1)
                value = value.Substring(0, index);
            comments = comments.Replace(match.Value, value);
        }
        foreach(Match match in Regex.Matches(comments, @"(`[1-9]*)"))
        {
            comments = comments.Replace(match.Value, string.Empty);
        }
        if(comments.StartsWith("Gets "))
            comments = comments.Substring(5);
        comments = char.ToUpper(comments[0]) + comments.Substring(1);
        return comments;
    }

    string NamespaceDeclaration(Class c)
    {
        StringBuilder output = new StringBuilder();
        output.AppendLine(@"/*
 * Copyright © 2022-Present The Open Human Task Specification Authors. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0(the ""License"");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an ""AS IS"" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
");
        output.AppendLine(@"/* -----------------------------------------------------------------------
 * This file has been automatically generated by a tool
 * -----------------------------------------------------------------------
 */
");
    System.IO.DirectoryInfo aggregateDirectory = new System.IO.FileInfo(((File)c.Parent).FullName).Directory;
        output.AppendLine($"namespace OpenHumanTask.Runtime.Integration.Commands.{aggregateDirectory.Name}");
        output.AppendLine("{");
        return output.ToString();
    }

    string ClassDeclaration(Class c)
    {
        StringBuilder output = new StringBuilder();
        if(c.DocComment != null)
        {
            output.AppendLine(Indent(1, "/// <summary>"));
            output.AppendLine(Indent(1, $"/// {SanitizeDocComments(c.DocComment)}"));
            output.AppendLine(Indent(1, "/// </summary>"));
        }
        output.AppendLine(Indent(1, "[DataContract]"));
        Attribute discriminatorAttribute = c.Attributes.FirstOrDefault(a => a.Name == "Discriminator");
        Attribute discriminatorValueAttribute = c.Attributes.FirstOrDefault(a => a.Name == "DiscriminatorValue");
        if(c.BaseClass != null && c.BaseClass.Name.StartsWith("AggregateRoot"))
            output.AppendLine(Indent(1, "[Queryable]"));
        if(c.IsAbstract && discriminatorAttribute != null)
            output.AppendLine(Indent(1, $"[Discriminator(nameof({discriminatorAttribute.Value}))]"));
        else if(!c.IsAbstract && discriminatorValueAttribute != null)
           output.AppendLine(Indent(1, $"[DiscriminatorValue({discriminatorValueAttribute.Value.Replace("OpenHumanTask.Runtime.Integration.", string.Empty).Replace("OpenHumanTask.Runtime.", string.Empty)})]"));
        output.Append(Indent(1, $"public "));
        if(c.IsAbstract)
            output.Append("abstract ");
        output.Append("partial ");
        output.AppendLine($"class {GetClassName(c)}");
        if(c.BaseClass != null)
            output.AppendLine(Indent(2, $": {GetBaseClass(c)}"));
        output.AppendLine(Indent(1, "{"));
        return output.ToString();
    }

    string PropertyDeclarations(Class c)
    {
        StringBuilder output = new StringBuilder();
        var order = 1;
        foreach(Property property in c.Properties.Where(p => !p.Attributes.Any(a => a.Name == "ProjectNever") || p.Attributes.Any(a => a.Name == "Map")))
        {
            output.AppendLine();
            if(property.DocComment != null)
            {
                output.AppendLine(Indent(2, "/// <summary>"));
                output.AppendLine(Indent(2, $"/// {SanitizeDocComments(property.DocComment)}"));
                output.AppendLine(Indent(2, "/// </summary>"));
            }
            Attribute jsonPropertyAttribute = property.Attributes.FirstOrDefault(a => a.Name == "JsonProperty");
            var propertyName = jsonPropertyAttribute == null ? property.Name : jsonPropertyAttribute.Arguments.First().Value.ToString();
            output.AppendLine(Indent(2, $"[DataMember(Name = \"{Char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1)}\", Order = {order})]"));
            if(jsonPropertyAttribute != null)
            {
                output.AppendLine(Indent(2, $"[Newtonsoft.Json.JsonProperty({jsonPropertyAttribute.Value})]"));
                output.AppendLine(Indent(2, $"[System.Text.Json.Serialization.JsonPropertyName(\"{propertyName}\")]"));
                output.AppendLine(Indent(2, $"[YamlDotNet.Serialization.YamlMember(Alias = \"{propertyName}\")]"));
            }  
            if(property.DocComment != null)
                output.AppendLine(Indent(2, $@"[Description(""{SanitizeDocComments(property.DocComment)}"")]"));
            if(property.Type.OriginalName == "Metadata")
            {
                output.AppendLine(Indent(2, "[Newtonsoft.Json.JsonExtensionData]"));
                output.AppendLine(Indent(2, "[System.Text.Json.Serialization.JsonExtensionData]"));
            }
            List<string> attributes = new List<string>(property.Attributes.Count);
            if(property.Attributes.Any(a => a.Name == "Required"))
                attributes.Add("Required");
            Attribute attribute = property.Attributes.FirstOrDefault(a => a.Name == "MinLength");
            if(attribute != null)
                attributes.Add($"MinLength({string.Join(", ", attribute.Arguments.Select(a => a.Value))})");
            if(attributes.Any())
                output.AppendLine(Indent(2, $"[{string.Join(", ", attributes)}]"));
            output.AppendLine(Indent(2, $"public virtual {GetType(property.Type)} {property.Name} {{ get; set; }}"));
            order++;
        }
        return output.ToString();
    }

}$Classes(OpenHumanTask.Runtime.Application.Commands.*Command)[$NamespaceDeclaration
$ClassDeclaration$PropertyDeclarations
    }

}]
