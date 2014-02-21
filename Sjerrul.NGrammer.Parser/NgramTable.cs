using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.NGrammer.Parser
{
    internal class NgramTable
    {
        private IList<Ngram> _ngrams;

        public NgramTable()
        {
            _ngrams = new List<Ngram>();
        }

        internal void Add(string key, string value)
        {
            Ngram ngram = _ngrams.SingleOrDefault(x => x.Key == key);

            if (ngram == null)
            {      
                ngram = new Ngram(key, value);

                _ngrams.Add(ngram);               
            }
            else
            {
                ngram.AddValue(value);
            }
        }

        internal int GetNumberOfNgrams()
        {
            return _ngrams.Count();
        }

        internal string GetRandomKey()
        {
            IList<Ngram> shuffled = _ngrams.Select(x => new { ngram = x, guid = Guid.NewGuid() }).OrderBy(y => y.guid).Select(x => x.ngram).ToList();
            Ngram random = shuffled.First();

            return random.Key;
        }

        internal string GetRandomValue(string key)
        {
            Ngram ngram = _ngrams.SingleOrDefault(x => x.Key == key);

            if (ngram == null)
            {
                return null;
            }

            return ngram.GetRandomValue();
        }
    }
}
