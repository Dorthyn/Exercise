using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using trans;

namespace Server
{
    public partial class ServerForm : Form
    {
        static int _port = Int32.Parse("5536");
        private string _path = "";

        public ServerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //创建监听线程
            // Create a TCPListener to accept client connections
            TcpListener listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();

            TcpClient client = listener.AcceptTcpClient(); // Get client connection


            NetworkStream clientStream = client.GetStream();
            //BinaryReader br = new BinaryReader(clientStream);

            byte[] pathBytes = new byte[client.ReceiveBufferSize];
            int bytesRead = clientStream.Read(pathBytes, 0, client.ReceiveBufferSize);
            _path = Encoding.UTF8.GetString(pathBytes, 0, bytesRead);



            //BinaryReader src = new BinaryReader(new BufferedStream(client.GetStream()));
            //int stringLength = src.Read(); // Returns an unsigned byte as an int
            //if (stringLength == -1)
            //    throw new EndOfStreamException();
            //byte[] stringBuf = new byte[stringLength];
            //src.Read(stringBuf, 0, stringLength);


            //拿到文件路径，判断存在性
            //_path = System.Text.Encoding.Default.GetString(pathBytes);

            label1.Text = _path;

            if (File.Exists(_path))
            {
                //文件存在，需要返回命令帧
                Mode.ServerCmd serverCmd = new Mode.ServerCmd();
                //文件存在
                serverCmd.IsExist = true;

                //获取文件大小
                System.IO.FileInfo f = new FileInfo(_path);
                serverCmd.Length = f.Length;

                //发送给客户端，写一个同样大小的空文件，以dat结尾作调试用

                byte[] bytesToSend = Convertor.StructToBytes(serverCmd);
                client.GetStream().Write(bytesToSend, 0, bytesToSend.Length);

                //服务器收到从客户端传来的准备好的消息

                Mode.ClientCmd clientCmd = new Mode.ClientCmd();

                byte[] structBytes = new byte[client.ReceiveBufferSize];
                clientStream.Read(structBytes, 0, client.ReceiveBufferSize);
                Mode.ClientCmd cmdFromClient = (Mode.ClientCmd)Convertor.BytesToStruct(structBytes, clientCmd.GetType());

                //测试是否准备好——客户端已经建好空文件，记得校验md5
                //MessageBox.Show(cmdFromClient.IsReady.ToString());

                //开始发送数据
                //1.创建一个预指令
                Mode.PreData preData = new Mode.PreData();
                //文件剩余传输量
                preData.Left = serverCmd.Length;
                //每次传输量
                preData.Period = 1024;

                //文件对象
                FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read);

                //读文件，从当前位置开始
                //当前位置为0
                int curPos = 0;

                while (preData.Left > 0)
                {
                    clientStream.Read(structBytes, 0, client.ReceiveBufferSize);
                    cmdFromClient = (Mode.ClientCmd)Convertor.BytesToStruct(structBytes, clientCmd.GetType());

                    //客户端准备好接收数据
                    if(cmdFromClient.IsReady)
                    {
                        //命令字节数组
                        //byte[] cmdBytes = Convertor.StructToBytes(preData);

                        //如果剩余量比每次传输的片段值大，证明还可以按原来的量传输
                        if (preData.Left > preData.Period)
                        {
                            //本次传送完成后剩余文件大小
                            preData.Left -= preData.Period;

                            //命令字节数组
                            byte[] cmdBytes = Convertor.StructToBytes(preData);

                            byte[] periodbytes = new byte[preData.Period];

                            //文件大小超过4G要怎么办？？？暂时先取int的范围
                            fs.Read(periodbytes, 0, periodbytes.Length);
                            //文件流现在在periodbytes里面，和命令拼接后发送给客户端


                            string tmp = Encoding.UTF8.GetString(periodbytes);
                            byte[] sumBytes = new byte[cmdBytes.Length + periodbytes.Length];
                            System.Buffer.BlockCopy(cmdBytes, 0, sumBytes, 0, cmdBytes.Length);
                            System.Buffer.BlockCopy(periodbytes, 0, sumBytes, cmdBytes.Length, periodbytes.Length);

                            //将拼接后的字节数组发出去
                            client.GetStream().Write(sumBytes, 0, sumBytes.Length);
                            client.GetStream().Flush();
                            //更新当前位置
                            curPos += preData.Period;
                            fs.Position = curPos;
                        }
                        else
                        {
                            //传差量
                            preData.Period = (int)preData.Left;

                            //本次传送完成后剩余文件大小
                            preData.Left -= preData.Period;
                            //命令字节数组
                            byte[] cmdBytes = Convertor.StructToBytes(preData);

                            byte[] periodbytes = new byte[preData.Period];
                            fs.Read(periodbytes, 0, periodbytes.Length);
                            //写完了
                            //文件流现在在periodbytes里面，和命令拼接后发送给客户端
                            byte[] sumBytes = new byte[cmdBytes.Length + periodbytes.Length];
                            System.Buffer.BlockCopy(cmdBytes, 0, sumBytes, 0, cmdBytes.Length);
                            System.Buffer.BlockCopy(periodbytes, 0, sumBytes, cmdBytes.Length, periodbytes.Length);
                            //将拼接后的字节数组发出去

                            string tmp = Encoding.UTF8.GetString(periodbytes);
                            curPos += preData.Period;

                            client.GetStream().Write(sumBytes, 0, sumBytes.Length);
                            client.GetStream().Flush();
                            fs.Position = curPos;
                        }
                    }
                }

                //关闭文件
                fs.Close();
                MessageBox.Show("传输完成！");
                //MessageBox.Show("文件存在");
            }
            else
            {
                MessageBox.Show("无此文件");
            }
            client.Close();
            listener.Stop();
        }

    }
}
