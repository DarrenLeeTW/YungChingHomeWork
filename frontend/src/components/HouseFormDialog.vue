<template>
  <el-dialog
    v-model="visible"
    :title="isEdit ? '編輯房屋' : '新增房屋'"
    width="600px"
    :close-on-click-modal="false"
    @close="handleClose"
  >
    <el-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      label-width="80px"
      class="house-form"
    >
      <el-form-item label="名稱" prop="name">
        <el-input
          v-model="formData.name"
          placeholder="請輸入房屋名稱"
          clearable
        />
      </el-form-item>

      <el-form-item label="地址" prop="address">
        <el-input
          v-model="formData.address"
          placeholder="請輸入房屋地址"
          clearable
        />
      </el-form-item>

      <el-form-item label="價格" prop="price">
        <el-input-number
          v-model="formData.price"
          :min="0"
          :step="10000"
          :precision="0"
          controls-position="right"
          class="price-input"
        />
        <span class="price-unit">元</span>
      </el-form-item>
    </el-form>

    <template #footer>
      <div class="dialog-footer">
        <el-button @click="handleCancel">取消</el-button>
        <el-button type="primary" @click="handleSave" :loading="saving">
          {{ isEdit ? '更新' : '新增' }}
        </el-button>
      </div>
    </template>
  </el-dialog>
</template>

<script setup>
import { ref, computed, watch } from 'vue'
import { ElMessage } from 'element-plus'

// Props
const props = defineProps({
  modelValue: Boolean,
  house: Object,
  isEdit: Boolean
})

// Emits
const emit = defineEmits(['update:modelValue', 'save', 'cancel'])

// 響應式數據
const formRef = ref()
const saving = ref(false)

const formData = ref({
  name: '',
  address: '',
  price: 0
})

const rules = {
  name: [
    { required: true, message: '請輸入房屋名稱', trigger: 'blur' },
    { min: 2, max: 50, message: '名稱長度在 2 到 50 個字符', trigger: 'blur' }
  ],
  address: [
    { required: true, message: '請輸入房屋地址', trigger: 'blur' },
    { min: 5, max: 100, message: '地址長度在 5 到 100 個字符', trigger: 'blur' }
  ],
  price: [
    { required: true, message: '請輸入房屋價格', trigger: 'blur' },
    { type: 'number', min: 1, message: '價格必須大於 0', trigger: 'blur' }
  ]
}

// 計算屬性
const visible = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value)
})

// 監聽器
watch(() => props.house, (newHouse) => {
  if (newHouse) {
    formData.value = {
      id: newHouse.id,
      name: newHouse.name,
      address: newHouse.address,
      price: newHouse.price
    }
  } else {
    resetForm()
  }
}, { immediate: true })

// 方法
const resetForm = () => {
  formData.value = {
    name: '',
    address: '',
    price: 0
  }
  if (formRef.value) {
    formRef.value.clearValidate()
  }
}

const handleSave = async () => {
  if (!formRef.value) return

  try {
    await formRef.value.validate()
    saving.value = true
    emit('save', { ...formData.value })
  } catch (error) {
    ElMessage.warning('請檢查表單填寫是否正確')
  } finally {
    saving.value = false
  }
}

const handleCancel = () => {
  emit('cancel')
  resetForm()
}

const handleClose = () => {
  emit('update:modelValue', false)
  resetForm()
}
</script>

<style scoped>
.house-form {
  padding: 20px 0;
}

.price-input {
  width: 200px;
}

.price-unit {
  margin-left: 8px;
  color: #909399;
  font-size: 14px;
}

.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}
</style>