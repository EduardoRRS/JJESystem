using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JJE.Mensagens;

namespace JJE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LimparCampos();
            this.CarregarGrade();
        }

        private void PreencherCampos()
        {
            if (this.txtCodigo.Text == "")
            {
                this.LimparCampos();
            }
            else
            {
                int Codigo = Int32.Parse(this.txtCodigo.Text);
                Pessoa pessoa = new ModelContext().PessoaSet.Where(p => p.Id == Codigo).FirstOrDefault();
                if (pessoa == null)
                {
                    MessageBox.Show("Registro não encontrado!");
                    this.LimparCampos();
                    this.txtCodigo.Focus();
                }
                else
                {
                    this.txtNome.Text = pessoa.Nome;
                    this.txtEndereco.Text = pessoa.Endereco;
                    this.lblDataCadastro.Text = pessoa.Data_Cadastro.ToString("dd/MM/yyyy hh:mm:ss");
                    this.txtTelefone.Text = pessoa.Telefone;
                }
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                var context = new ModelContext();
                var pessoa = new Pessoa();

                pessoa.Data_Cadastro = DateTime.Now;
                pessoa.Nome = txtNome.Text;
                pessoa.Endereco = txtEndereco.Text;
                pessoa.Telefone = txtTelefone.Text;

                if (this.txtCodigo.Text == "")
                {
                    context.PessoaSet.Add(pessoa);
                }
                else
                {
                    pessoa.Id = Int32.Parse(this.txtCodigo.Text);
                    context.Entry(pessoa).State = System.Data.Entity.EntityState.Modified;
                }
                context.SaveChanges();

                this.txtCodigo.Text = pessoa.Id.ToString();

                MessageBox.Show("Registro cadastrado com sucesso!");

                this.CarregarGrade();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (this.txtCodigo.Text == "")
            {
                MessageBox.Show("Informe o registro que deseja excluir!");
            }
            else
            {
                int codigo = Int32.Parse(this.txtCodigo.Text);
                var context = new ModelContext();
                var pessoa = context.PessoaSet.Where(F => F.Id == codigo).FirstOrDefault();
                context.PessoaSet.Remove(pessoa);
                context.SaveChanges();

                MessageBox.Show("Registro excluído com sucesso!");
                this.LimparCampos();
                this.CarregarGrade();
            }
        }

        private void LimparCampos()
        {
            this.txtCodigo.Text = "";
            this.txtEndereco.Text = "";
            this.txtNome.Text = "";
            this.txtTelefone.Text = "";
            this.lblDataCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        }

        private void CarregarGrade()
        {
            dtGrid.DataSource = new ModelContext().PessoaSet.OrderBy(q => q.Nome).ToList();
        }

        private void txtCodigo_LostFocus(object sender, EventArgs e)
        {
            this.PreencherCampos();
        }

        private void dtGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1) { return; }

            this.txtCodigo.Focus();
            this.txtCodigo.Text = dtGrid.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            SendKeys.Send("{tab}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.LimparCampos();
        }
    }
}