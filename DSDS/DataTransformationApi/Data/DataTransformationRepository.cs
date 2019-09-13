namespace DataTransformationApi.Data
{
    public class DataTransformationRepository : IDataTransformationRepository
    {
        private readonly DataContext _context;

        public DataTransformationRepository(DataContext context)
        {
            _context = context;
        }
    }
}