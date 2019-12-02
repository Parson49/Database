using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelInfo
{
    public class Character
    {
        public enum Affiliation
        {
            None,
            Gusu_Lan,
            Yunmeng_Jiang,
            Lanling_Jin,
            Qishan_Wen,
            Qinghe_Nie
        }
        #region FIELDS

        private string _name;
        private string _courtName;
        private int _age;
        private Affiliation _clan;
        private bool _status;

        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string CourtesyName
        {
            get { return _courtName; }
            set { _courtName = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public Affiliation Clan
        {
            get { return _clan; }
            set { _clan = value; }
        }

        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Character()
        {

        }

        public Character(string name, string courtname, int age, Affiliation Clan, bool status)
        {
            _name = name;
            _courtName = courtname;
            _age = age;
            _clan = Clan;
            _status = status;

        }

        #endregion
    }
}
