<template>
  <div id="app">
    <ProductCodeManager />
  </div>
</template>

<script>
  import ProductCodeManager from './components/ProductControl.vue'

  export default {
    name: 'App',
    components: {
      ProductCodeManager
    }
  }
</script>

<style>
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }

  body {
    font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;
  }

  #app {
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
  }
</style>
