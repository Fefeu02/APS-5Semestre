using System.Windows.Forms;
using System.Net;
using System;

namespace ChatServidor
{
    public partial class Form1 : Form
    {
        private delegate void AtualizaStatusCallback(string strMensagem);

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAtender_Click(object sender, System.EventArgs e)
        {
            if (txtIP.Text==string.Empty)
            {
                MessageBox.Show("Informe o endereço IP.");
                txtIP.Focus();
                return;
            }

            try
            {

                IPAddress enderecoIP = IPAddress.Parse(txtIP.Text);

                ChatServidor mainServidor = new ChatServidor(enderecoIP);

                ChatServidor.StatusChanged += new StatusChangedEventHandler(mainServidor_StatusChanged);

                mainServidor.IniciaAtendimento();

                txtLog.AppendText("Monitorando as conexões...\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conexão : " + ex.Message);
            }
        }

        public void mainServidor_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            this.Invoke(new AtualizaStatusCallback(this.AtualizaStatus), new object[] { e.EventMessage });
        }

        private void AtualizaStatus(string strMensagem)
        {
            txtLog.AppendText(strMensagem + "\r\n");
        }
    }
}
