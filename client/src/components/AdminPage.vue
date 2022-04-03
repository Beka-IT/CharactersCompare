<template>
  <div id="adminPage">
    <div>
      <h1 class="header">Добавить героя:</h1>
    </div>
    <form @submit.prevent="addHero">
          <input name="fullname" id="fullname" v-model="fullname" placeholder="Имя персонажа">
          <input name="photoLink" id="photoLink" v-model="photoLink" placeholder="Ссылка на фото героя" type="text">  
          <input name="rating" id="rating" v-model="rating" placeholder="Рейтинг героя" type="number">
          <input class="btn" type="submit" value="Готово">
    </form>

    <div>
      <h1 class="header">Список героев:</h1>
    </div>
    <div>
        <div  v-for="hero in heroes" >
          <div id="elements">
            <img width="80" height="80" v-bind:src="hero.photoLink"/>
            <div class="text">{{ hero.fullname}}</div>
            <div class="text">{{hero.rating}}</div>
            <button @click="deleteHero(hero.id)" class="delete">Удалить</button>
          </div>
          <hr/>
        </div>
    </div>
  </div>
</template>
<script>

export default {
  name: "AdminPage",
  data() {
    return {
      responeFromServer:"",
      wasRequest:false,
      id: 0,
      fullname: "",
      photoLink: "",
      rating: 0,
      heroes: [],
   }
  } ,
  mounted: function mounted () {
    if(localStorage.getItem('tokens') == null || localStorage.getItem('tokens')=="undefined"){
      window.location.href = 'http://localhost:3000/#/admin';
    }
    this.getAllHeroes()
  },
  methods: {
    async getAllHeroes(){
      fetch("https://localhost:5001/api/Character",{
        method: 'GET',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + JSON.stringify(window.localStorage.getItem('tokens'))
        }
      })
      .then( response => response.json() )
      .then( response => {
          this.heroes = response;
      } );
    },
    async deleteHero(heroId){
      fetch( `https://localhost:5001/api/Character/${heroId}`,{
          method: 'GET',
          headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + JSON.stringify(window.localStorage.getItem('tokens'))
        }
      } )
      .then( response => response.json() )
      .then( response => {
          this.getAllHeroes()
      } );
    },
    async addHero() {
      
        const { id,photoLink, fullname, rating} = this;
        const res = await fetch(
          "https://localhost:5001/api/Character",
          {
            method: "POST",
            headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + JSON.stringify(window.localStorage.getItem('tokens'))
          },
            body: JSON.stringify({
              id,
              fullname,
              photoLink,
              rating,
            })
          }
        ).then(response=>{
          this.responeFromServer = response.statusText
          let el = document.getElementById("photoLink");
          el.value = "";
          el.dispatchEvent(new Event('input'));
          let el2 = document.getElementById("fullname");
          el2.value = "";
          el2.dispatchEvent(new Event('input'));
          let el3 = document.getElementById("rating");
          el3.value = "100";
          el3.dispatchEvent(new Event('input'));
            this.getAllHeroes()
            return response.json()
          })
        let data = res.json()
        console.log(data)
        
      }
    }
    
  
};
</script>

<style scoped> 
#adminPage{
  background-color: white;
  margin-left: auto;
  margin-right: auto;
  margin-top: 20px;
  border-radius: 10px;
  width: 90%;
  overflow: visible;
}
.text{
  margin-left: 30px;
  width: 200px;
  color: green;
  font-size: 20px;
}
.delete{
  margin-left: 10px;
  margin-top: 5px;
  padding: 10px;
  height: 40px;
  color: gold;
  background-color: #FF392E;
  border-radius: 10px;
  border: 2px solid #FF392E;
}
.delete:hover{
  background-color: gold;
  color:#FF392E;
}
.header{
  text-align: center;
  color: #3CAEA3;
}
#elements{
  display: flex;
  margin-left: 270px;
}
form {
    width: 100%;
    margin-left: auto;
    margin-right: auto;
    padding: 20px;
    padding-right: 0px;    
    padding-left: 0px;
    border-radius: 10px;
    justify-content: space-around;
}
input, select {
    margin-top: 5px;
    margin-left: 20px;
    padding: 10px 6px;
    width: 280px;
    box-sizing: bordre-box;
    border: 1px solid #3CAEA3;
    border-radius: 4px;

}

input[type="checkbox"] {
    width:16px;
    margin-left: 20px;
    position: relative;
    top: 2px;
}


.btn {
    width: 150px;
    color: white;
    background-color: #007bff;
    border-radius: 10px;
    margin: auto;
    margin-top: 5px;
    margin-left: 20px;
}



</style>



Hope this is helpful 🙂
Post navigation
← Previous Post
Next Post →
