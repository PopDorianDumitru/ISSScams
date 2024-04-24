using ISSProject.Common;
using ISSProject.GraphAnalyser.Domain;

namespace ISSProject.GraphAnalyser.Repository
{
    internal interface IGraphAnalyserLogRepository : IRepository<GraphAnalyserLog, int>
    {
        IEnumerable<GraphAnalyserLog> All();
        GraphAnalyserLog ById(int id);
        bool Delete(GraphAnalyserLog entity);
        bool Insert(GraphAnalyserLog entity);
        bool Update(GraphAnalyserLog entity);
    }
}