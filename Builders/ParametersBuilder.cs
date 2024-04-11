using System.Collections.Generic;

namespace ToPdf.Builders
{
    public class ParametersBuilder<T>
    {
        private readonly ToPdfService _service;
        public Dictionary<T, object> Parameters;

        public ParametersBuilder(ToPdfService service)
        {
            _service = service;
        }

        public ToPdfService WithParameters(Dictionary<T, object> parameters)
        {
            Parameters = parameters;
            return _service;
        }
    }
}