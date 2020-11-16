﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace DurableTask.Netherite
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using DurableTask.Core;

    class ActivityWorkItem : TaskActivityWorkItem
    {
        public Partition Partition { get; set; }

        // the partition for the orchestration that issued this activity
        public uint OriginPartition { get; set; }

        // a partition-local identifier for this activity (is a sequence number generated by ActivitiesState)
        public long ActivityId { get; set; }


        public string OriginWorkItem { get; set; }

        public ActivityWorkItem(Partition partition, long activityId, TaskMessage message, string originWorkItem)
        {
            this.Partition = partition;
            this.OriginPartition = partition.PartitionFunction(message.OrchestrationInstance.InstanceId);
            this.ActivityId = activityId;
            this.OriginWorkItem = originWorkItem;
            this.Id = activityId.ToString();
            this.LockedUntilUtc = DateTime.MaxValue; // this backend does not require workitem lock renewals
            this.TaskMessage = message;
        }

        public string WorkItemId => ActivitiesState.GetWorkItemId(this.Partition.PartitionId, this.ActivityId);

        public string ExecutionType => (this.Partition.PartitionId == this.OriginPartition ? "Local" : "Remote");
    }
}
