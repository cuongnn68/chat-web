<template>
  <div class="parent">
    <div class="child">
      <div class="info">
        <h2>User info</h2>
        <div class="in">
          <label for="full-name">Full name</label>
          <input name="full-name" type="text">
        </div>
        <div class="in">
          <label for="email">Email</label>
          <input name="email" type="text">
        </div>
        <div class="in">
          <label for="phone">Phone</label>
          <input name="phone" type="text">
        </div>
        <button class="btn">Update</button>
      </div>
      <div class="pass">
        <h2>Change password</h2>
        <div class="in">
          <label for="old-pass">Old password</label>
          <input name="old-pass" type="password">
        </div>
        <div class="in">
          <label for="re-old-pass">Repeat old passwrod</label>
          <input name="re-old-pass" type="password">
        </div>
        <div class="in">
          <label for="new-pass">New password</label>
          <input name="new-pass" type="password">
        </div>
        <button class="btn">Update</button>

      </div>
    </div>
  </div>
</template>

<script>
export default {

}
</script>

<style scoped>
  .btn {
    align-self: center;
    margin: 0.4rem;
  }
  .parent {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 100%;
    width: 100%;
  }
  .child {
    display: flex;  
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }
  .info {
    display: flex;
    flex-direction: column;
    
  }
  .pass {
    display: flex;
    flex-direction: column;
  }
  .in {
    margin: 0.4rem;
    display: flex;
    flex-direction: column;
    align-items: flex-start;
  }

</style>