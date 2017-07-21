namespace studentManagementInformationSystem
{
    /// <summary>
    /// 学生信息类
    /// </summary>
    public class StudentInformationClass
    {
        private string _classNo;//班级号
        private string _studentNo;//学号
        private string _nameStudent;//学生姓名
        private int _gradeStudent;//成绩

        private string _sex;//学生姓名

        private string _birthdayStudent;//生日

        public string NoClass
        {
            set { this._classNo = value; }
            get { return this._classNo; }
        }

        public string StudentNo
        {
            set { this._studentNo = value; }
            get { return this._studentNo; }
        }

        public string NameStudent
        {
            set { this._nameStudent = value; }
            get { return this._nameStudent; }
        }

        public int GradeStudent
        {
            set { this._gradeStudent = value; }
            get { return this._gradeStudent; }
        }

        public string Sex
        {
            set { this._sex = value; }
            get { return this._sex; }
        }

        public string BirthdayStudent
        {
            set { this._birthdayStudent = value; }
            get { return _birthdayStudent; }
        }
    }
}
