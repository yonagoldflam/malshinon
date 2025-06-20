﻿using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon.moddels
{
    public class IntelReport
    {
        public int Id { get; set; }
        public int ReporterId { get; set; }
        public int TargetId { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public IntelReport(int RId, int TId, string text, DateTime time = default(DateTime), int id = 0)
        {
            ReporterId = RId;
            TargetId = TId;
            Text = text;
            Timestamp = time;
            Id = id;

        }

    }
}
