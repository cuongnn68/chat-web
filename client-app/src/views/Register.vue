<template>
  <div class="parrent">
    <div class="child">
      <p>Register</p>
      <div>
        <label for="username">Username</label>
        <input name="username" type="text">
      </div>
      <div>
        <label for="password">Password</label>
        <input name="password" type="text">
      </div>
      <div>
        <label for="email">Email</label>
        <input name="email" type="text">
      </div>
      <div>
        <label for="address">Address</label>
        <input name="address" type="text">
      </div>
      <div>
        <label for="phone">Phone Number</label>
        <input name="phone" type="text">
      </div>
      <p>Date of Birth</p>
      <button>Register</button>
    </div>
  </div>
</template>

<script>
export default {

}
</script>
  
<style scoped>
  .parrent{
    display: flex;
    justify-content: center;
    align-items: center;
  }
  .child {
    border-radius: 24px;
    border-width: 10px;
    border-color: black;
  }
  .line {
    /* width: 250px; */
    display: flex;
    flex-direction: row;
    justify-content: center;
    /* justify-content: space-evenly; */
  }
  .inline1 {
    width: 85px;
    flex-basis: 1;
  }
  .inline2 {
    flex-basis: 4;
  }
  .mrg-h {
    margin: 20px 0px 0px 0px;
  }
</style>