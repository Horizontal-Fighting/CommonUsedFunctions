using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Service
{
    public class MappingService : IMappingService
    {
        protected MapperConfiguration Configuration { get; }
        protected IMapper Mapper { get; }
        public MappingService()
        {
            Configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<AutoMapperProfile>();
            });
            Mapper = Configuration.CreateMapper();
        }
        public TR Map<TS, TR>(TS source)
        {
            var destinationType = Mapper.Map<TS, TR>(source);
            return destinationType;
        }
    }
}
