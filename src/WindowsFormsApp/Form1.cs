using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.Model;
using WindowsFormsApp.Repository;
using WindowsFormsApp.Repository.Interfaces;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        private readonly ILoginContext _loginContext;
        private readonly IWeatherForecastContext _weatherForecastContext;

        private Usuario _usuario;

        public Form1()
        {
            InitializeComponent();

            _loginContext = new LoginContext();

            _weatherForecastContext = new WeatherForecastContext();

            _usuario = new Usuario();
        }        

        private async void Login_Click(object sender, EventArgs e)
        {
            _usuario.Nomeusuario = txtUsuario.Text;
            _usuario.Password = txtSenha.Text;

            _usuario = await _loginContext.EfetuarLogin(_usuario);

            DadosLogin();
        }

        private void DadosLogin()
        {
            if (_usuario.Success)
            {
                lblLogado.ForeColor = System.Drawing.Color.LightGreen;

                lblLogado.Text = "Sim";
            }
            else
            {
                lblLogado.ForeColor = System.Drawing.Color.Red;

                lblLogado.Text = "Não";

                if(_usuario.ErrorMenssage != null && _usuario.ErrorMenssage.Any())
                    InformarErro(_usuario.ErrorMenssage);
            }

            txtToken.Visible = _usuario.Success;

            txtToken.Text = _usuario.Token;
        }

        private void InformarErro(List<string> errorMenssage)
        {
            var erros = "Erros encontrados:";

            foreach (var item in errorMenssage)
            {
                erros += $"\n{item}";
            }

            MessageBox.Show(erros, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void Buscar_Click(object sender, EventArgs e)
        {
            IEnumerable<Temperatura> temperaturas = null;

            try
            {
                temperaturas = await _weatherForecastContext.ObterTemepraturas(_usuario.Token);                
            }
            catch (Exception err)
            {
                InformarErro(new List<string>(1) { err.Message });

                temperaturas = null;
            }

            dataGridView.DataSource = temperaturas;

            dataGridView.Refresh();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            _usuario = new Usuario();

            DadosLogin();
        }
    }
}
