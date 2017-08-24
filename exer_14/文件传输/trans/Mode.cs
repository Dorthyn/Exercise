namespace trans
{
    class Mode
    {
        public struct ServerCmd
        {
            //文件是否存在
            public bool IsExist;
            //文件总长度
            public long Length;
        }

        public struct ClientCmd
        {
            //客户端转备好写文件
            public bool IsReady;
        }

        public struct PreData
        {
            //本次传输片段大小
            public int Period;
            //剩余文件大小
            public long Left;
        }
        /// <summary>
        /// 暂时没用到
        /// </summary>
        private enum Type
        {
            Cmd,
            Data
        }
    }
}
