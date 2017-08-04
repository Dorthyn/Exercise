using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simonsimon
{
    public class UsersInfo
    {
        private string _nameUsers;//用户姓名
        private string _pswUsers;//用户密码
        private int _level1Score = 0;//简单模式下的得分
        private int _level2Score = 0;//中等模式下的得分
        private int _level3Score = 0;//困难模式下的得分

        #region Field

        public string NameUsers
        {
            set { this._nameUsers = value; }
            get { return _nameUsers; }
        }

        public string PswUsers
        {
            set { this._pswUsers = value; }
            get { return _pswUsers; }
        }

        public int Level1Score
        {
            get { return _level1Score; }
            set { this._level1Score = value; }
        }

        public int Level2Score
        {
            get { return _level2Score; }
            set { this._level2Score = value; }
        }

        public int Level3Score
        {
            get { return _level3Score; }
            set { this._level3Score = value; }
        }

        #endregion

    }
}
