using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.NGrammer.Parser
{
    internal class Ngram
    {
        public string Key { get; private set; }
        private IList<string> _values;


        internal Ngram(string key, string value)
        {
            Key = key;

            _values = new List<string>();
            _values.Add(value);
        }

        internal void AddValue(string value)
        {
            _values.Add(value);

        }

        internal string GetRandomValue()
        {
            IList<string> shuffled = _values.Select(x => new { value = x, guid = Guid.NewGuid() }).OrderBy(y => y.guid).Select(x => x.value).ToList();
            return shuffled.First();
        }
    }
}
