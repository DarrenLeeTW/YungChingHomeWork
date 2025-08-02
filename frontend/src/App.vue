<template>
  <div id="app">
    <el-container class="app-container">
      <!-- 頂部導航 -->
      <el-header class="app-header">
        <div class="header-content">
          <div class="logo">
            <el-icon size="32" color="#409eff">
              <House />
            </el-icon>
            <h1 class="app-title">房屋列表管理系統</h1>
          </div>
          <div class="header-actions">
            <el-button type="primary" @click="showAddDialog = true">
              <el-icon><Plus /></el-icon>
              新增房屋
            </el-button>
          </div>
        </div>
      </el-header>

      <!-- 主要內容區域 -->
      <el-main class="app-main">
        <HouseListingManager 
          :show-add-dialog="showAddDialog"
          @close-add-dialog="showAddDialog = false"
        />
      </el-main>
    </el-container>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { House, Plus } from '@element-plus/icons-vue'
import HouseListingManager from './components/HouseListingManager.vue'

const showAddDialog = ref(false)
</script>

<style scoped>
.app-container {
  height: 100vh;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

.app-header {
  background: rgba(255, 255, 255, 0.95);
  backdrop-filter: blur(10px);
  border-bottom: 1px solid rgba(255, 255, 255, 0.2);
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.header-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 100%;
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 20px;
}

.logo {
  display: flex;
  align-items: center;
  gap: 12px;
}

.app-title {
  font-size: 24px;
  font-weight: 600;
  color: #2c3e50;
  margin: 0;
}

.app-main {
  padding: 20px;
  max-width: 1200px;
  margin: 0 auto;
}

.header-actions {
  display: flex;
  gap: 12px;
}
</style>