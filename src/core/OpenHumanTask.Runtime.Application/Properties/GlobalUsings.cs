﻿// Copyright © 2022-Present The Open Human Task Specification Authors. All rights reserved.
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

global using CloudNative.CloudEvents;
global using Microsoft.Extensions.Logging;
global using Neuroglia;
global using Neuroglia.Data;
global using Neuroglia.Eventing;
global using Neuroglia.Mapping;
global using Neuroglia.Mediation;
global using OpenHumanTask.Runtime.Application.Configuration;
global using OpenHumanTask.Runtime.Domain.Models;
global using OpenHumanTask.Runtime.Infrastructure.Services;
global using OpenHumanTask.Sdk.Models;
global using System.Net.Mime;
