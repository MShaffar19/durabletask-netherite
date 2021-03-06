﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


namespace DurableTask.Netherite.Faster
{
    using Newtonsoft.Json;
    using System;

    [JsonObject]
    class CheckpointInfo
    {
        [JsonProperty]
        public Guid IndexToken { get; set; }

        [JsonProperty]
        public Guid LogToken { get; set; }

        [JsonProperty]
        public long CommitLogPosition { get; set; }

        [JsonProperty]
        public long InputQueuePosition { get; set; }

        internal void CopyFrom(CheckpointInfo other)
        {
            this.IndexToken = other.IndexToken;
            this.LogToken = other.LogToken;
            this.CommitLogPosition = other.CommitLogPosition;
            this.InputQueuePosition = other.InputQueuePosition;
        }
    }
}
