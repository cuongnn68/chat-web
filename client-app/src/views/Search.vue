<template>
  <div class="parent">
    <div class="child">
      <h1 class="space-top">Search chat rooms, users</h1>
      <div class="search-bar space-top">
        <select class="border-w" name="type" v-model="type">
          <option value="" disabled>Type</option>
          <option value="room">Room</option>
          <option value="user">User</option>
        </select>
        <input class="border-w" type="text">
        <button class="border-w search-btn">
          <i class="fas fa-search"></i>
          Search
        </button>
      </div>
      <div class="result space-top">
        <table>
          <tr v-if="type==='room'">
            <th>Id</th>
            <th class="long-rd">Name</th>
            <th>Time Created</th>
            <th></th>
          </tr>
          <tr v-if="type==='user'">
            <th>Id</th>
            <th>Username</th>
            <th class="long-rd">Fullname</th>
            <th></th>
          </tr>

          <!-- RM cant use v-if with v-for -->
          <!-- TODOclear list after search -->
          <tr v-for="user in users" v-bind:key="user.id">
            <td>{{user.id}}</td>
            <td>{{user.username}}</td>
            <td>{{user.fullName}}</td>
            <td>//TODO Detail </td>
          </tr>

          <tr v-for="room in rooms" v-bind:key="room.id">
            <td>{{room.id}}</td>
            <td>{{room.name}}</td>
            <td>{{room.timeCreated}}</td>
            <td>//TODO joinRoom button </td>
          </tr>
          
        </table>
      </div>
      <!-- <h3 v-if="users===[]&&rooms===[]" class="space-top">No result<br>Find some chat room or user</h3> -->
      <!-- TODO show no result -->
    </div>
  </div>
</template>

<script>
export default {
  name: "Search",
  data() {
    return {
      type: "room",
      users: [
        {
          id: 1,
          username: "user1",
          fullName: "name1",
        },
        {
          id: 2,
          username: "user2",
          fullName: "name2",
        },
        {
          id: 3,
          username: "user3",
          fullName: "name3",
        },
        {
          id: 4,
          username: "user4",
          fullName: "name4",
        },
        {
          id: 5,
          username: "user5",
          fullName: "name5",
        },
        {
          id: 6,
          username: "user6",
          fullName: "name6",
        },
        {
          id: 7,
          username: "user7",
          fullName: "name7",
        },
      ],
      rooms: [
        {
          id:11,
          name: "room1",
          timeCreated: "day1",
        },
        {
          id:22,
          name: "room2",
          timeCreated: "day2",
        },
        {
          id:33,
          name: "room3",
          timeCreated: "day3",
        },
        {
          id:44,
          name: "room4",
          timeCreated: "day4",
        },
      ]
    }
  }
}
</script>

<style scoped>
  
  .child {
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    /* align-items: flex-start; */
  }
  .space-top {
    margin: 40px 0 0 0;
  }
  .search-bar {
    /* background-color: red; */
    /* margin: 40px 0 40px; */
  }
  .result {
    /* background-color: blue; */
    /* flex-grow: 1; */
    display: flex;
    flex-direction: columns;
    justify-content: center;
  }
  .border-w {
    border-width: 0.00000000000000000000000000000001rem;
    border-color: gray;
  }
  .search-btn {
    border-radius: 0 10px 10px 0;
    border-left-style: none;
    
    /* border-color: black;
    border-width: 0.01rem; */
    /* border-top-right-radius: 20%;
    border-bottom-right-radius: 10px; */
    background-color: lightgray;
    box-shadow: none;
  }
  .search-btn:hover {
    background-color: #b15ebb;

  }
  
  input {
    border-radius: 0;
    border-right-style: none;
    border-left-style: none;
    box-shadow: none;
  }
  select {
    font-weight: 600;
    /* color: red; */
    /* border-radius; */
    outline: none;
    font-size: 17px;
    padding: 0.4rem;
    background-color: lightgray;
    border-radius: 10px 0 0 10px;
    border-right-style: none;
  }
  option {
    /* background-color: red; */
    font-size: 17px;
    height: 100px;
    /* margin: 0.4rem; */
  }
  .long-rd {
    width: 250px;
  }
</style>