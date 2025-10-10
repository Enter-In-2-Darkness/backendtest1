<template>
  <div class="container">
    <h1 class="title">ระบบจัดการสินค้า</h1>

    <!-- Form Card -->
    <div class="form-card">
      <div class="form-group">
        <label for="productCodeInput">รหัสสินค้า (30 ตัวอักษร)</label>
        <input
          v-model="productCode"
          @input="handleInput"
          type="text"
          placeholder="XXXXX-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"
          maxlength="35"
          class="input-field"
          id="productCodeInput"
        />
        <p v-if="error" class="error-message">{{ error }}</p>
        <p class="hint">กรอกเฉพาะตัวเลข 0-9 และตัวอักษร A-Z เท่านั้น</p>
      </div>
      <button @click="handleAdd" class="btn-add">
        
        <span class="icon">➕</span> เพิ่มสินค้า
      </button>
    </div>

    <!-- Table Card -->
    <div class="table-card">
      <table>
        <thead>
          <tr>
            <th>ลำดับ</th>
            <th>รหัสสินค้า</th>
            <th>วันที่เพิ่ม</th>
            <th>จัดการ</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="loading">
            <td colspan="4" class="loading-message">กำลังโหลดข้อมูล...</td>
          </tr>
          <tr v-else-if="products.length === 0">
            <td colspan="4" class="empty-message">ยังไม่มีข้อมูลรหัสสินค้า</td>
          </tr>
          <tr v-else v-for="(product, index) in products" :key="product.id">
            <td>{{ index + 1 }}</td>
            <td class="code-cell">{{ product.numberCode }}</td>
            <td>{{ formatDate(product.createAt) }}</td>
            <td class="action-cell">
              <button @click="showQR(product.numberCode)" class="btn-qr">
                📱 QR
              </button>
              <button @click="confirmDelete(index, product.id)" class="btn-delete">
                🗑️ ลบ
              </button>
            </td>
          </tr>
        </tbody>
      </table>
      <p v-if="products.length > 0" class="count">
        ทั้งหมด {{ products.length }} รายการ
      </p>
    </div>

    <!-- QR Modal -->
    <div v-if="showQRModal" class="modal" @click="closeQRModal">
      <div class="modal-content" @click.stop>
        <div class="modal-header">
          <h3>QR Code</h3>
          <button @click="closeQRModal" class="close-btn">&times;</button>
        </div>
        <div class="qr-container">
          <img 
            :src="`https://api.qrserver.com/v1/create-qr-code/?size=200x200&data=${encodeURIComponent(selectedCode)}`"
            alt="QR Code"
          />
          <p>{{ selectedCode }}</p>
        </div>
        <button @click="closeQRModal" class="btn-close-modal">ปิด</button>
      </div>
    </div>

    <!-- Confirm Dialog -->
    <div v-if="showConfirmDialog" class="modal" @click="cancelDelete">
      <div class="modal-content small" @click.stop>
        <h3>ยืนยันการลบ</h3>
        <p>คุณต้องการลบรหัสสินค้านี้ใช่หรือไม่?</p>
        <div class="modal-actions">
          <button @click="cancelDelete" class="btn-cancel">ยกเลิก</button>
          <button @click="handleDelete" class="btn-confirm-delete">ลบ</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import productService from '@/services/productService';

export default {
  name: 'ProductControl',
  data() {
    return {
      productCode: '',
      products: [],
      loading: false,
      error: '',
      showQRModal: false,
      selectedCode: '',
      showConfirmDialog: false,
      deleteIndex: null,
      deleteId: null
    };
  },
  async mounted() {
    await this.loadProducts();
  },
  methods: {
    formatProductCode(value) {
      const cleaned = value.toUpperCase().replace(/[^0-9A-Z]/g, '');
      const limited = cleaned.slice(0, 30);
      const groups = [];
      for (let i = 0; i < limited.length; i += 5) {
        groups.push(limited.slice(i, i + 5));
      }
      return groups.join('-');
    },
    handleInput(event) {
      this.productCode = this.formatProductCode(event.target.value);
      this.error = '';
    },
    validateProductCode(code) {
      const cleanCode = code.replace(/-/g, '');
      
      if (cleanCode.length !== 30) {
        return 'Product code maximun 30 characters';
      }
      
      if (!/^[0-9A-Z]{30}$/.test(cleanCode)) {
        return 'product code with alphabet and number only';
      }
      
      if (this.products.some(p => p.numberCode === code)) {
        return 'Product code in used';
      }
      
      return null;
    },
    async handleAdd() {
      const validationError = this.validateProductCode(this.productCode);
      
      if (validationError) {
        this.error = validationError;
        return;
      }
      
      try {
        this.loading = true;
        await productService.create(this.productCode);
        await this.loadProducts();
        this.productCode = '';
        this.error = '';
      } catch (error) {
        this.error = error.response?.data?.message || 'wrong activity';
      } finally {
        this.loading = false;
      }
    },
    async loadProducts() {
      try {
        this.loading = true;
        this.products = await productService.getAll();
      } catch (error) {
        console.error('Error loading products:', error);
        this.error = 'Can not load data';
      } finally {
        this.loading = false;
      }
    },
    showQR(code) {
      this.selectedCode = code;
      this.showQRModal = true;
    },
    closeQRModal() {
      this.showQRModal = false;
    },
    confirmDelete(index, id) {
      this.deleteIndex = index;
      this.deleteId = id;
      this.showConfirmDialog = true;
    },
    async handleDelete() {
      try {
        this.loading = true;
        await productService.delete(this.deleteId);
        await this.loadProducts();
        this.showConfirmDialog = false;
        this.deleteIndex = null;
        this.deleteId = null;
      } catch (error) {
        console.error('Error deleting product:', error);
        this.error = 'can not delete data';
      } finally {
        this.loading = false;
      }
    },
    cancelDelete() {
      this.showConfirmDialog = false;
      this.deleteIndex = null;
      this.deleteId = null;
    },
    formatDate(date) {
      return new Date(date).toLocaleString('th-TH');
    }
  }
};
</script>

<style scoped>
.container {
  width: 100%;
  margin: 0 auto;
  padding: 2rem;
  background-color: #f7f8fc;
  min-height: 100vh;
}

.title {
  font-size: 2rem;
  color: #333;
  margin-bottom: 2rem;
  text-align: center;
}

.form-card {
  background: white;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.05);
  margin-bottom: 2rem;
  max-width: 900px;
  margin-left: auto;
  margin-right: auto;
}

.form-group {
  margin-bottom: 1.5rem;
}

.form-card .form-group:last-of-type {
  margin-bottom: 0;
}

label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: 500;
  color: #555;
}

.input-field {
  width: 100%;
  padding: 0.75rem;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 1rem;
  font-family: 'Courier New', Courier, monospace;
  transition: border-color 0.2s, box-shadow 0.2s;
}

.input-field:focus {
  outline: none;
  border-color: #4a90e2;
  box-shadow: 0 0 0 3px rgba(74, 144, 226, 0.2);
}

.hint {
  font-size: 0.875rem;
  color: #666;
  margin-top: 0.25rem;
}

.error-message {
  color: #f44336;
  font-size: 0.875rem;
  margin-top: 0.5rem;
}

.btn-add {
  background: #4a90e2;
  color: white;
  border: none;
  padding: 0.8rem 1.5rem;
  border-radius: 6px;
  cursor: pointer;
  font-size: 1rem;
  font-weight: 500;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  width: 100%;
  transition: background-color 0.2s;
}

.btn-add .icon { font-size: 0.8em; }

.btn-add:hover {
  background: #357abd;
}

.table-card {
  background: white;
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.05);
  overflow: hidden;
  max-width: 900px;
  margin-left: auto;
  margin-right: auto;
}

table {
  width: 100%;
  border-collapse: collapse;
}

thead {
  background: #f7f8fc;
}

th {
  padding: 1rem;
  text-align: left;
  font-weight: 600;
  color: #555;
  text-transform: uppercase;
  font-size: 0.875rem;
}

td {
  padding: 1rem;
  border-top: 1px solid #eee;
  border-right: 1px solid #eee;
  color: #333;
}

th:last-child, td:last-child {
  border-right: none;
}

.code-cell {
  font-family: 'Courier New', Courier, monospace;
  font-weight: 500;
}

.action-cell {
  display: flex;
  gap: 0.5rem;
}

.btn-qr, .btn-delete {
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 0.9rem;
  transition: background-color 0.2s;
}

.btn-qr {
  background: #50e3c2;
  color: white;
}

.btn-qr:hover {
  background: #0b7dda;
}

.btn-delete {
  background: #ff7675;
  color: white;
}

.btn-delete:hover {
  background: #da190b;
}

.empty-message {
  text-align: center;
  color: #888;
  padding: 3rem !important;
}

.count {
  padding: 1rem;
  color: #666;
  font-size: 0.875rem;
}

/* Modal Styles */
.modal {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0,0,0,0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.modal-content {
  background: white;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 10px 30px rgba(0,0,0,0.1);
  max-width: 500px;
  width: 90%;
  color: #333;
  position: relative;
}

.modal-content.small {
  max-width: 400px;
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-bottom: 1rem;
  border-bottom: 1px solid #eee;
  margin-bottom: 1.5rem;
}

.close-btn {
  position: absolute;
  top: 1rem;
  right: 1rem;
  background: transparent;
  border: 0;
  font-size: 1.5rem;
  cursor: pointer;
  color: #999;
}

.qr-container {
  text-align: center;
}

.qr-container img {
  width: 200px;
  height: 200px;
  margin-bottom: 1rem;
}

.qr-container p {
  font-family: 'Courier New', Courier, monospace;
  word-break: break-all;
  color: #666;
}

.btn-close-modal {
  display: none; /* Hidden, use the top-right X button */
  background: #2196F3;
  color: white;
  border: none;
  padding: 0.75rem;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 1rem;
}

.modal-actions {
  display: flex;
  justify-content: flex-end;
  gap: 1rem;
  margin-top: 1.5rem;
}

.btn-cancel, .btn-confirm-delete {
  flex: 1;
  padding: 0.75rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-weight: 500;
}

.btn-cancel {
  background: #e0e0e0;
  color: #333;
}

.btn-confirm-delete {
  background: #d0021b;
  color: white;
}

@media (min-width: 768px) {
  .form-card {
    display: flex;
    gap: 1rem;
    align-items: flex-end;
  }
  .form-group {
    flex: 1;
    margin-bottom: 0;
  }
  .btn-add { width: auto; }
}

</style>
