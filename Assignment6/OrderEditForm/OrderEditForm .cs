using OrderSystem.Core;

namespace OrderEditForm
{
    public partial class OrderEditForm : Form
    {
        private readonly Order _originalOrder;
        private Order _workingOrder;
        private BindingSource _bsOrder = new BindingSource();
        private BindingSource _bsDetails = new BindingSource();

        public Order ResultOrder { get; private set; }

        public OrderEditForm(Order order = null)
        {
            InitializeComponent();
            _originalOrder = order;
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            _workingOrder = (Order)(_originalOrder?.Clone() ?? new Order("", ""));
            _bsOrder.DataSource = _workingOrder;
            _bsDetails.DataSource = _bsOrder;
            _bsDetails.DataMember = "Details";

            txtOrderId.DataBindings.Add("Text", _bsOrder, "OrderId", true);
            txtCustomer.DataBindings.Add("Text", _bsOrder, "Customer", true);
            dgvEditDetails.DataSource = _bsDetails;
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            using var detailForm = new DetailEditForm();
            if (detailForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _workingOrder.AddDetail(detailForm.Detail);
                    _bsDetails.ResetBindings(false);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "´íÎó", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                ResultOrder = _workingOrder;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
