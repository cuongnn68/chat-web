<template>
  <div>
    <input type="text" v-model="content">
    <!-- RM v-on:click="methodName" without bracket () -->
    <button v-on:click="newNoti">
      click
    </button>
  </div>
</template>

<script>
export default {
  name: "testnoti",
  data() {
    return {
      content: ""
    }
  },
  methods: {
    newNoti() {
      if(!this.content) return;
      this.$emit("newNoti", {content: this.content, type: ""});
    }
  }
}
</script>

<style>

</style>