using System;

namespace AppMo
{
    public class pair<t, k>
    {
        public t first { get; set; }
        public k second { get; set; }
        public pair(t item1, k item2)
        {
            first = item1;
            second = item2;
        }
    }
}
