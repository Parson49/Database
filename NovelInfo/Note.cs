using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelInfo
{
    class Note
    {
        #region FIELDS

        private string _name;
        private string _anote;

        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string ANote
        {
            get { return _anote; }
            set { _anote = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Note()
        {

        }

        public Note(string name, string ANote)
        {
            _name = name;
            _anote = ANote;
        }

        #endregion
    }
}
