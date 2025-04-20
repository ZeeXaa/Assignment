using OrderApp;
using OrderApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderForm {
    public partial class FormEdit : Form {
        private OrderService orderService;
        public bool EditFlag { get; set; }

        public Order CurrentOrder { get; set; }
        public event Action<FormEdit> CloseEditFrom = (f => { });

        public FormEdit(Order order, bool editFlag, OrderService orderService) {
            InitializeComponent();
            bdsCustomers.Add(new Customer("1", "li"));
            bdsCustomers.Add(new Customer("2", "zhang"));
            
            //TODO 如果想实现不点保存只关窗口后订单不变化，需要把order深克隆给CurrentOrder
            this.CurrentOrder = order;
            bdsOrders.DataSource = CurrentOrder;
            this.orderService = orderService;

            this.EditFlag = editFlag;
            txtOrderId.Enabled = !editFlag;
            if (!editFlag) {
                order.Customer = bdsCustomers.Current as Customer;
            }
        }

        private void btnAddDetail_Click(object sender, EventArgs e) {
            FormDetailEdit formDetailEdit = new FormDetailEdit(new OrderDetail());
            try {
                if (formDetailEdit.ShowDialog() == DialogResult.OK) {
                    int index = 0;
                    if (CurrentOrder.Details.Count != 0) {
                        index = CurrentOrder.Details.Max(i => i.Index) + 1;
                    }
                    formDetailEdit.Detail.Index = index;
                    CurrentOrder.AddDetail(formDetailEdit.Detail);
                    bdsDetails.ResetBindings(false);
                }
            } catch (Exception e2) {
                MessageBox.Show(e2.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 可选验证：是否至少有一个订单项
                if (CurrentOrder.Details.Count == 0)
                {
                    MessageBox.Show("订单至少应包含一个订单项！");
                    return;
                }

                // 保存到数据库
                var repo = new OrderRepository();
                repo.SaveOrder(CurrentOrder);  // ← 将订单保存到 MySQL

                // 内存中同步保存（比如保存到订单列表）
                if (this.EditFlag)
                {
                    orderService.UpdateOrder(CurrentOrder);
                }
                else
                {
                    orderService.AddOrder(CurrentOrder);
                }

                MessageBox.Show("订单保存成功！");
                CloseEditFrom(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message);
            }
        }


        private void btnEditDetail_Click(object sender, EventArgs e) {
            EditDetail();
        }

        private void dgvDetail_DoubleClick(object sender, EventArgs e) {
            EditDetail();
        }

        private void EditDetail() {
            OrderDetail detail = bdsDetails.Current as OrderDetail;
            if (detail == null) {
                MessageBox.Show("请选择一个订单项进行修改");
                return;
            }
            FormDetailEdit formDetailEdit = new FormDetailEdit(detail);
            if (formDetailEdit.ShowDialog() == DialogResult.OK) {
                bdsDetails.ResetBindings(false);
            }
        }

        private void btnDeleteDetail_Click(object sender, EventArgs e) {
            OrderDetail detail = bdsDetails.Current as OrderDetail;
            if (detail == null) {
                MessageBox.Show("请选择一个订单项进行删除");
                return;
            }
            CurrentOrder.RemoveDetail(detail);
            bdsDetails.ResetBindings(false);
        }


    }
}
