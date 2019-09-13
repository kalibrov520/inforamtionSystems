using System;
using System.Threading.Tasks;
using Models;

namespace DataTransformationApi.Data
{
    public class DataTransformationRepository : IDataTransformationRepository
    {
        private readonly DataContext _context;

        public DataTransformationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task PostDataFeedInfoAsync(DataFeedInfo info)
        {
            try
            {
                await _context.DataFeedInfo.AddAsync(info);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //ignored 
            }
        }
    }
}