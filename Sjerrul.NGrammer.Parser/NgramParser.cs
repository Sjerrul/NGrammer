using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sjerrul.NGrammer.Parser
{
    public class NgramParser
    {
        private int _n;
        private NgramTable _table;

        private NgramParser()
        {
            _table = new NgramTable();      
        }

        public NgramParser(int n) : this()
        {
            _n = n;
        }

        public void Read(string text)
        {
            text = text.Replace(System.Environment.NewLine, " ");
            text = this.CompactWhitespaces(text);

            string[] splitted = text.Split(' ');

            for (int i = 0; i < splitted.Length - 2; i++)
            {
                string key = splitted[i] + ' ' + splitted[i+1];
                string value = splitted[i+2];

                _table.Add(key, value);
            }
        }

        public string Write()
        {
            if (_table.GetNumberOfNgrams() == 0)
            {
                return String.Empty;
            }

            //First get started
            string text = String.Empty;
            string key = _table.GetRandomKey();
            string value = _table.GetRandomValue(key);
            text += key + " " + value;

            //Get the last two words as a new key
            string[] parts = text.Split(' ');
            string newKey = parts[parts.Length - 2] + ' ' + parts[parts.Length - 1];

            //Get new value for new key
            value = _table.GetRandomValue(newKey);

            while (value != null)
            {                
                text += ' ' + value;

                parts = text.Split(' ');
                newKey = parts[parts.Length - 2] + ' ' + parts[parts.Length - 1];
                value = _table.GetRandomValue(newKey);
            }
            
            return text.ToString();
        }

        public int GetNumberOfNgrams()
        {
            return _table.GetNumberOfNgrams();
        }

        private String CompactWhitespaces(String s)
        {
            StringBuilder sb = new StringBuilder(s);

            CompactWhitespaces(sb);

            return sb.ToString();
        }

        private void CompactWhitespaces(StringBuilder sb)
        {
            if (sb.Length == 0)
                return;

            // set [start] to first not-whitespace char or to sb.Length

            int start = 0;

            while (start < sb.Length)
            {
                if (Char.IsWhiteSpace(sb[start]))
                    start++;
                else
                    break;
            }

            // if [sb] has only whitespaces, then return empty string

            if (start == sb.Length)
            {
                sb.Length = 0;
                return;
            }

            // set [end] to last not-whitespace char

            int end = sb.Length - 1;

            while (end >= 0)
            {
                if (Char.IsWhiteSpace(sb[end]))
                    end--;
                else
                    break;
            }

            // compact string

            int dest = 0;
            bool previousIsWhitespace = false;

            for (int i = start; i <= end; i++)
            {
                if (Char.IsWhiteSpace(sb[i]))
                {
                    if (!previousIsWhitespace)
                    {
                        previousIsWhitespace = true;
                        sb[dest] = ' ';
                        dest++;
                    }
                }
                else
                {
                    previousIsWhitespace = false;
                    sb[dest] = sb[i];
                    dest++;
                }
            }

            sb.Length = dest;
        }
    }
}
