using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public static QuanLiThuCungEntities2 db = new QuanLiThuCungEntities2();
        public Form1()
        {
            InitializeComponent();
            initForm();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Xác nhận đóng chương trình","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        private void initForm()
        {
            dtpNgayNhan.Value = DateTime.Now;
            rdbChuaBenh.Checked = true;
            checkRdb();
            LoadDataToListView();
        }
        private void checkRdb()
        {
            if(rdbChuaBenh.Checked)
            {
                txtChiPhiThuoc.Visible = true;
                lblChiPhiThuoc.Visible = true;
                txtSoNgay.Visible = false;
                lblSoNgay.Enabled = false;
                
            }
            else
            {
                txtSoNgay.Visible = true;
                lblSoNgay.Enabled = true;
                txtChiPhiThuoc.Visible = false;
                lblChiPhiThuoc.Visible = false;
            }
        }

        private void rdbChuaBenh_CheckedChanged(object sender, EventArgs e)
        {
            checkRdb();
        }

        private void rdbChamSocHo_CheckedChanged(object sender, EventArgs e)
        {
            checkRdb();
        }
        private void reset()
        {
            initForm();
            txtMaDon.Text = string.Empty;
            txtTenThuCung.Text = string.Empty;
            txtChungLoai.Text = string.Empty;
            txtCanNang.Text = string.Empty;
            txtTinhTrang.Text = string.Empty;
            txtSoNgay.Text = string.Empty;
            txtChiPhiThuoc.Text = string.Empty;
            txtMaDon.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            reset();
            
        }
        private void addDatabaseCBTC(ChuaBenhThuCung cbtc)
        {
            db.ChuaBenhThuCungs.Add(cbtc);
            db.SaveChanges();
            MessageBox.Show("Lưu vào database thành công!", "Thông báo");
        }
        private void addDatabaseCSHTC(ChamSocHoThuCung cshtc)
        {
            db.ChamSocHoThuCungs.Add(cshtc);
            db.SaveChanges();
            MessageBox.Show("Lưu vào database thành công!", "Thông báo");
        }
        private void addChuaBenh()
        {
            
            if(rdbChuaBenh.Checked )
            {
                
                if (isValidData())
                {
                    ChuaBenhThuCung cbtc = new ChuaBenhThuCung()
                    {
                        MaDon = txtMaDon.Text,
                        TenThuCung = txtTenThuCung.Text,
                        ChungLoai = txtChungLoai.Text,
                        CanNang = int.Parse(txtCanNang.Text),
                        TinhTrang = txtTinhTrang.Text,
                        NgayNhan = dtpNgayNhan.Value,
                        PhiDieuTri = decimal.Parse(txtChiPhiThuoc.Text)
                    };
                    addDatabaseCBTC(cbtc);
                    string[] data = { cbtc.MaDon, cbtc.TenThuCung, cbtc.ChungLoai, cbtc.NgayNhan.ToString("dd-MM-yyyy") };
                    ListViewItem newCBTC = new ListViewItem(data);
                    lsvDanhSachThuCung.Items.Add(newCBTC);
                    if (cbtc.CanNang > 40)
                    {  
                        lsvDanhSachThuCung.Items[newCBTC.Index].BackColor = Color.Yellow;
                        lsvDanhSachThuCung.Items[newCBTC.Index].Selected = true;
                    }
                }
            }
            else
            {
                if (isValidData())
                {
                    ChamSocHoThuCung cshtc = new ChamSocHoThuCung()
                    {
                        MaDon = txtMaDon.Text,
                        TenThuCung = txtTenThuCung.Text,
                        ChungLoai = txtChungLoai.Text,
                        CanNang = int.Parse(txtCanNang.Text),
                        TinhTrang = txtTinhTrang.Text,
                        NgayNhan = dtpNgayNhan.Value,
                        SoNgay = int.Parse(txtSoNgay.Text)
                    };
                    addDatabaseCSHTC(cshtc);
                    string[] data = { cshtc.MaDon, cshtc.TenThuCung, cshtc.ChungLoai, cshtc.NgayNhan.ToString("dd-MM-yyyy") };
                    ListViewItem newCSHTC = new ListViewItem(data);
                    lsvDanhSachThuCung.Items.Add(newCSHTC);
                    if (cshtc.CanNang > 40)
                    {
                        lsvDanhSachThuCung.Items[newCSHTC.Index].BackColor = Color.Yellow;
                        lsvDanhSachThuCung.Items[newCSHTC.Index].Selected = true;
                    }
                }
            }
            
        }

       

        private void LoadDataToListView()
        {
            lsvDanhSachThuCung.Items.Clear();

            foreach(var tc in db.ChuaBenhThuCungs)
            {
                ListViewItem item = new ListViewItem(tc.MaDon);
                item.SubItems.Add(tc.TenThuCung);
                item.SubItems.Add(tc.ChungLoai);
                item.SubItems.Add(tc.NgayNhan.ToString("dd-MM-yyyy"));

                

                lsvDanhSachThuCung.Items.Add(item);
                if (tc.CanNang > 40)
                {
                    lsvDanhSachThuCung.Items[item.Index].BackColor = Color.Yellow;
                    lsvDanhSachThuCung.Items[item.Index].Selected = true;
                }
            }
            foreach (var tc in db.ChamSocHoThuCungs)
            {
                ListViewItem item = new ListViewItem(tc.MaDon);
                item.SubItems.Add(tc.TenThuCung);
                item.SubItems.Add(tc.ChungLoai);
                item.SubItems.Add(tc.NgayNhan.ToString("dd-MM-yyyy"));

               

                lsvDanhSachThuCung.Items.Add(item);
                if (tc.CanNang > 40)
                {
                    lsvDanhSachThuCung.Items[item.Index].BackColor = Color.Yellow;
                    lsvDanhSachThuCung.Items[item.Index].Selected = true;
                }
            }
        }
        private bool isValidData()
        {
            if (string.IsNullOrWhiteSpace(txtMaDon.Text))
            {
                MessageBox.Show("Mã đơn không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenThuCung.Text))
            {
                MessageBox.Show("Tên thú cưng không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtChungLoai.Text))
            {
                MessageBox.Show("Chủng loại không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCanNang.Text))
            {
                MessageBox.Show("Cân nặng không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTinhTrang.Text))
            {
                MessageBox.Show("tình trạng không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if(dtpNgayNhan.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày nhận không được lớn hơn ngày hiện tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if(rdbChuaBenh.Checked)
            {
                if(string.IsNullOrWhiteSpace(txtChiPhiThuoc.Text))
                {
                    MessageBox.Show("Chi phí thuốc không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtSoNgay.Text))
                {
                    MessageBox.Show("Số ngày không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            addChuaBenh();
        }

        private void grbThongTinThuCung_Enter(object sender, EventArgs e)
        {

        }

        private void lsvDanhSachThuCung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lsvDanhSachThuCung.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvDanhSachThuCung.SelectedItems[0];
                item.BackColor = Color.AliceBlue;
                ChuaBenhThuCung cbtc = db.ChuaBenhThuCungs.Find(item.SubItems[0].Text);
                if(cbtc != null)
                {
                    txtMaDon.Text = cbtc.MaDon;
                    txtTenThuCung.Text = cbtc.TenThuCung;
                    txtChungLoai.Text = cbtc.ChungLoai;
                    txtCanNang.Text = cbtc.CanNang.ToString();
                    txtTinhTrang.Text = cbtc.TinhTrang;
                    rdbChuaBenh.Checked = true;
                    txtChiPhiThuoc.Text = cbtc.PhiDieuTri.ToString();
                    checkRdb();
                }
                ChamSocHoThuCung cshtc = db.ChamSocHoThuCungs.Find(item.SubItems[0].Text);
                if (cshtc != null)
                {
                    txtMaDon.Text = cshtc.MaDon;
                    txtTenThuCung.Text = cshtc.TenThuCung;
                    txtChungLoai.Text = cshtc.ChungLoai;
                    txtCanNang.Text = cshtc.CanNang.ToString();
                    txtTinhTrang.Text = cshtc.TinhTrang;
                    rdbChamSocHo.Checked = true;
                    txtSoNgay.Text = cshtc.SoNgay.ToString();
                    checkRdb();
                }
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (lsvDanhSachThuCung.SelectedItems.Count > 0)
            {
                string MaDon = lsvDanhSachThuCung.SelectedItems[0].SubItems[0].Text;
                int selectedIndex = lsvDanhSachThuCung.SelectedIndices[0];
                DialogResult = MessageBox.Show("Bạn có chắc muốn xóa ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.Yes)
                {
                    if (rdbChuaBenh.Checked)
                    {
                        db.ChuaBenhThuCungs.Remove(db.ChuaBenhThuCungs.Find(MaDon));
                        db.SaveChanges();
                        lsvDanhSachThuCung.Items.RemoveAt(selectedIndex);
                        if (selectedIndex < lsvDanhSachThuCung.Items.Count - 1)
                        {
                            lsvDanhSachThuCung.Items[selectedIndex].Selected = true;
                        }
                        if (lsvDanhSachThuCung.Items.Count > 0)
                        {
                            lsvDanhSachThuCung.Items[lsvDanhSachThuCung.Items.Count - 1].Selected = true;
                        }
                        else
                        {
                            btnThem_Click(sender, e);
                        }
                    }
                    else
                    {
                        db.ChamSocHoThuCungs.Remove(db.ChamSocHoThuCungs.Find(MaDon));
                        db.SaveChanges();
                        lsvDanhSachThuCung.Items.RemoveAt(selectedIndex);
                        if (selectedIndex < lsvDanhSachThuCung.Items.Count - 1)
                        {
                            lsvDanhSachThuCung.Items[selectedIndex].Selected = true;
                        }
                        if (lsvDanhSachThuCung.Items.Count > 0)
                        {
                            lsvDanhSachThuCung.Items[lsvDanhSachThuCung.Items.Count - 1].Selected = true;
                        }
                        else
                        {
                            btnThem_Click(sender, e);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 dòng để xóa", "Thông báo");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            bool isUpdated = false;
            if(lsvDanhSachThuCung.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvDanhSachThuCung.SelectedItems[0];
                ChuaBenhThuCung cbtc = db.ChuaBenhThuCungs.Find(item.SubItems[0].Text);
                if(txtTenThuCung.Text != cbtc.TenThuCung)
                {
                    cbtc.TenThuCung = txtTenThuCung.Text;
                    item.SubItems[1].Text = txtTenThuCung.Text;
                    isUpdated = true;
                }
                if(txtChungLoai.Text != cbtc.ChungLoai)
                {
                    cbtc.ChungLoai = txtChungLoai.Text;
                    item.SubItems[2].Text = txtChungLoai.Text;
                    isUpdated = true;
                }
                if(int.Parse(txtCanNang.Text) != cbtc.CanNang)
                {
                    cbtc.CanNang = int.Parse(txtCanNang.Text);
                    isUpdated = true;
                }
                if(txtTinhTrang.Text != cbtc.TinhTrang)
                {
                    cbtc.TinhTrang = txtTinhTrang.Text;
                    isUpdated=true;
                }
                if(dtpNgayNhan.Value.ToString("dd-MM-yyyy") != cbtc.NgayNhan.ToString("dd-MM-yyyy"))
                {
                    cbtc.NgayNhan = dtpNgayNhan.Value;
                    item.SubItems[3].Text = dtpNgayNhan.Value.ToString("dd-MM-yyyy");
                    isUpdated = true;
                }
                if(decimal.Parse(txtChiPhiThuoc.Text) != cbtc.PhiDieuTri)
                {
                    cbtc.PhiDieuTri = decimal.Parse(txtChiPhiThuoc.Text);
                    isUpdated = true;
                }
                db.SaveChanges();
                if(isUpdated)
                {
                    MessageBox.Show("Cập nhật thành công", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Chưa có thông tin cập nhật được thay đổi","Cảnh báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Vui lòng chọn một dòng để sửa!", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        private void btnSapXep_Click(object sender, EventArgs e)
        {
            LoadDataToListView();
            TCCompare tcCompare  = new TCCompare();
            lsvDanhSachThuCung.ListViewItemSorter = tcCompare;
            lsvDanhSachThuCung.Sort();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            int SoLuongDonCB = db.ChuaBenhThuCungs.Count(tc => (tc.PhiDieuTri != 0));
            decimal PhiKham = 100000;
            decimal? TongDoanhThuCB;

            TongDoanhThuCB = db.ChuaBenhThuCungs.Where(tc => (tc.PhiDieuTri != 0)).Sum(tc => PhiKham + tc.PhiDieuTri);

            string thongBao = $"Doanh thu quan li thu cung: \n\n";
            thongBao += $"-{SoLuongDonCB} don chua benh. \n";
            thongBao += $"-{TongDoanhThuCB} VND - Doanh thu \n";
            MessageBox.Show(thongBao,"Thong ke doanh thu",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
