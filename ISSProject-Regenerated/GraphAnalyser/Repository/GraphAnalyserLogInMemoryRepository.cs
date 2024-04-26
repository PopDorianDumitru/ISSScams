using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.GraphAnalyser.Domain;
using ISSProject.GraphAnalyser.Repository;

namespace ISSProject_Regenerated.GraphAnalyser.Repository
{
    internal class GraphAnalyserLogInMemoryRepository : IGraphAnalyserLogRepository
    {
        private readonly List<IGraphAnalyserLog> graphsAnalyserLogs;

        public IEnumerable<GraphAnalyserLog> All()
        {
            return (IEnumerable<GraphAnalyserLog>)graphsAnalyserLogs.ToList();
        }

        public GraphAnalyserLog ById(int id)
        {
            return (GraphAnalyserLog)graphsAnalyserLogs.Find(graphsAnalyserLog => graphsAnalyserLog?.GetId() == id);
        }

        public bool Delete(GraphAnalyserLog entity)
        {
            graphsAnalyserLogs.Remove(entity);
            return true;
        }

        public bool Insert(GraphAnalyserLog entity)
        {
            graphsAnalyserLogs.Add(entity);
            return true;
        }

        public bool Update(GraphAnalyserLog entity)
        {
            for (int index = 0; index < graphsAnalyserLogs.Count; index++)
            {
                if (graphsAnalyserLogs[index].GetId == entity.GetId)
                {
                    graphsAnalyserLogs[index] = entity;
                    return true;
                }
            }
            return false;
        }
    }
}
