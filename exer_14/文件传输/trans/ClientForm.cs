using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace trans
{
    public partial class ClientForm : Form
    {
        string _downLoadPath = String.Empty;
        static string server = "127.0.0.1"; // Destination address
        private static int servPort = Int32.Parse("5536"); // Destination port


        public ClientForm()
        {
            InitializeComponent();
        }

        private void button_DownLoad_Click(object sender, EventArgs e)
        {
            _downLoadPath = textBox_path.Text;
            Thread thread = new Thread(new ParameterizedThreadStart(FileDownLoad));
            thread.IsBackground = true;
            thread.Start(_downLoadPath);
        }

        private void FileDownLoad(object path)
        {
            //文件路径
            string path_tmp = (string)path;

            TcpClient client = new TcpClient(server, servPort);
            NetworkStream netStream = client.GetStream();

            byte[] bt = System.Text.Encoding.UTF8.GetBytes(path_tmp);
            netStream.Write(bt,0,bt.Length);
            //BinaryWriter binaryWriter = new BinaryWriter(netStream);
            //binaryWriter.Write(path_tmp);



            //发送要下载的文件地址
            //byte[] pathTmpByteArray = System.Text.Encoding.UTF8.GetBytes(path_tmp);
            //netStream.Write(pathTmpByteArray, 0, pathTmpByteArray.Length);

            //接收服务器传回来的文件大小等数据
            //BinaryReader binaryReader = new BinaryReader(netStream);
            Mode.ServerCmd serverCmd = new Mode.ServerCmd();

            byte[] structBytes = new byte[client.ReceiveBufferSize];
            int structBytesLength = netStream.Read(structBytes, 0, client.ReceiveBufferSize);

            //////////////////////
            byte[] realStructBytes = structBytes.Skip(0).Take(structBytesLength).ToArray();

            //byte[] structBytes = StreamToBytes(client.GetStream());

            //结构体字节数组转结构体
            //structBytes = Convertor.BytesToStruct(structBytes, serverCmd.GetType());
            Mode.ServerCmd cmdFromServer = (Mode.ServerCmd)Convertor.BytesToStruct(realStructBytes, serverCmd.GetType());

            //cmdFromServer为从服务器拿到的数据
            //MessageBox.Show(cmdFromServer.IsExist.ToString());

            //创建用来接收文件的空文件
            //增加了一个out参数，返回当前文件的路径
            string dirPath = "";
            CreateFixedSizeFile("soso.dat", cmdFromServer.Length,out dirPath);

            //发信息给服务器，空文件已经建好
            Mode.ClientCmd clientCmd = new Mode.ClientCmd();
            clientCmd.IsReady = true;


            //打开文件，准备写文件（提前打开可能不安全）
            FileStream fs = new FileStream(dirPath + "soso.dat", FileMode.Open, FileAccess.Write);
            byte[] clientBytes = Convertor.StructToBytes(clientCmd);
            client.GetStream().Write(clientBytes, 0, clientBytes.Length);
            //fs.Seek(0, SeekOrigin.Begin);
            //开始接收数据
            //用break打断循环

            //当前写到的位置
            int curWrite = 0;

            while (true)
            {
                //把当前收到的字节数组分割为结构体和剩下的数据部分，而结构体的大小是知道的
                //覆盖填写，先测试不支持断点的，即本地不记录当前下载位置，在V0.2改进
                //收到的每一段均存在receiveBytes数组中
                byte[] receiveBytes = new byte[client.ReceiveBufferSize];

                //给服务器发通知，可以开始写下一波（第一波）了
                clientCmd.IsReady = true;
                clientBytes = Convertor.StructToBytes(clientCmd);
                client.GetStream().Write(clientBytes, 0, clientBytes.Length);

                int receiveBytesLength = netStream.Read(receiveBytes, 0, client.ReceiveBufferSize);

                clientCmd.IsReady = true;
                clientBytes = Convertor.StructToBytes(clientCmd);
                client.GetStream().Write(clientBytes, 0, clientBytes.Length);

                byte[] realReceiveBytes = receiveBytes.Skip(0).Take(receiveBytesLength).ToArray();

                //取前一部分转换成结构体
                byte[] cmdStructureBytes = Extension.SubArrayDeepClone(realReceiveBytes, 0, 16);
                //将字节流转换为结构体
                Mode.PreData preData = new Mode.PreData();

                //DataCmdFromClient为命令结构体
                Mode.PreData DataCmdFromServer = (Mode.PreData)Convertor.BytesToStruct(cmdStructureBytes, preData.GetType());


                //1 去后面的部分转换为字节流
                //byte[] dataBytes = realReceiveBytes.Skip(16).Take(DataCmdFromServer.Period + 16).ToArray();
                //byte[] dataBytes = new byte[1000];
                byte[] dataBytes = Extension.SubArrayDeepClone(realReceiveBytes, 16 , DataCmdFromServer.Period);


                string tmp_2 = Encoding.UTF8.GetString(dataBytes);
                //2 字节流转文件流
                fs.Write(dataBytes, 0, dataBytes.Length);

                //改变当前位置
                //curWrite += DataCmdFromServer.Period;
                //fs.Position = curWrite;

                //解析命令——Predata，包括本次传送的字节数和本次传输完成之后剩余的字节数
                //若果为0，则break掉
                if (DataCmdFromServer.Left == 0)
                {
                    fs.Close();
                    break;
                }

                //if (DataCmdFromServer.Left >= 0)
                //{
                //    //读取本次数据大小



                //    //为0时本次传送结束之后break掉
                //    if (0 == DataCmdFromServer.Left)
                //    {

                //    }
                //    else
                //    {

                //    }
                //}
            }

            netStream.Close();
            client.Close();
            MessageBox.Show("下载完成！");

        }
        /// <summary>
        /// 创建指定大小空文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileSize"></param>
        public static void CreateFixedSizeFile(string fileName, long fileSize,out string dir)
        {
            //验证参数 
            if (string.IsNullOrEmpty(fileName) || new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }.Contains(
                fileName[fileName.Length - 1]))
                throw new ArgumentException("fileName");
            if (fileSize < 0) throw new ArgumentException("fileSize");
            //创建目录 
            dir = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            //创建文件 
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                fs.SetLength(fileSize); //设置文件大小 
            }
            catch
            {
                if (fs != null)
                {
                    fs.Close();
                    File.Delete(fileName); //注意，若由fs.SetLength方法产生了异常，同样会执行删除命令，请慎用overwrite:true参数，或者修改删除文件代码。 
                }
                throw;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
    }
}
