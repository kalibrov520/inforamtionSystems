using System.Threading.Tasks;
using Camunda.Worker;

namespace CamundaUtils
{
    public interface ICamundaExecutionTaskResult
    {
        Task<IExecutionResult> GetExecutionResult();
    }
}