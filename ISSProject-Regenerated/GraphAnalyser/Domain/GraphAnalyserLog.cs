using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common;

namespace ISSProject.GraphAnalyser.Domain
{
    internal class GraphAnalyserLog : IKeyedEntity<int>, IGraphAnalyserLog
    {
        private readonly int logId;
        public int LogId
        {
            get { return logId; }
        }

        private readonly DateTime logTime;
        public DateTime LogTime
        {
            get { return logTime; }
        }

        private readonly int sourceUserId;
        public int SourceUserId
        {
            get { return sourceUserId; }
        }

        private readonly int targetUserId;
        public int TargetUserId
        {
            get { return targetUserId; }
        }

        private readonly int score;
        public int Score
        {
            get { return score; }
        }

        private readonly string generatedMessage;
        public string GeneratedMessage
        {
            get { return generatedMessage; }
        }

        public GraphAnalyserLog(int logId, DateTime logTime,
                                int sourceUserId, int targetUserId,
                                int score, string generatedMessage)
        {
            this.logId = logId;
            this.logTime = logTime;
            this.sourceUserId = sourceUserId;
            this.targetUserId = targetUserId;
            this.score = score;
            this.generatedMessage = generatedMessage;
        }

        public int GetId()
        {
            return logId;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
