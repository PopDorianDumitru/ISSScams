using ISSProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.GraphAnalyser.Domain
{
    internal class GraphAnalyserLog : IKeyedEntity<int>
    {
        private int _logId;
        public int LogId { get { return _logId; } }

        private DateTime _logTime;
        public DateTime LogTime { get { return _logTime;} }

        private int _sourceUserId;
        public int SourceUserId { get { return _sourceUserId;} }

        private int _targetUserId;
        public int TargetUserId { get { return _targetUserId; } }

        private int _score;
        public int Score { get { return _score; } }

        private string _generatedMessage;
        public string GeneratedMessage { get { return _generatedMessage; } }

        public GraphAnalyserLog(int logId, DateTime logTime,
                                int sourceUserId, int targetUserId, 
                                int score, string generatedMessage)
        {
            _logId = logId;
            _logTime = logTime;
            _sourceUserId = sourceUserId;
            _targetUserId = targetUserId;
            _score = score;
            _generatedMessage = generatedMessage;
        }

        public int GetId()
        {
            return _logId;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
