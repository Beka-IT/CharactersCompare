<script >
export default {
  data() {
    return {
        isStarted: false,
        isRating: false,
        heroes:[],
        c:1,
        heroes1:{},
    }
  },
  updated(){
      this.c = 1
  },
  methods:{
      back(){

        this.isRating = false;
      },
      choose(id){
          let idOfHeroes = []
          if(this.heroes1[0].id == id){
              idOfHeroes = [this.heroes1[0].id,this.heroes1[1].id]
              this.heroes1.pop()
          }else{
              idOfHeroes = [this.heroes1[1].id,this.heroes1[0].id]
              this.heroes1.shift()
          }
          fetch(
          "https://localhost:5001/api/MainPage",
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json"
            },
            body: JSON.stringify(idOfHeroes)
          }
        ).then(res=>res.json()).then(res=>this.heroes1.push(res))
      },
      startGame(){
          this.isStarted = true
          fetch("https://localhost:5001/api/MainPage/vs")
            .then( response => response.json() )
            .then( response => {
                this.heroes1 = response;
            } );
      },
      async getRating(){
        this.c = 1
        this.isRating = true;
        fetch("https://localhost:5001/api/MainPage")
        .then( response => response.json() )
        .then( response => {
            this.heroes = response;
        } );
    }
}
  
}
</script>

<template>
    <div id="mainPage">
        <div class="header">
            <input class="search" type="text" placeholder="Поиск..."/>
            <button class="searchbtn" >Искать</button>
            <button v-if="!isRating" @click="getRating()" class="rating" >Рейтинг Героев</button>
            <button v-if="isRating" @click="back()" class="rating" >Главная</button>
            
        </div>
        <div v-if="!isStarted && !isRating" class="startBlock1">
            <button @click="startGame()" class="startbtn">НАЧАТЬ!</button>
        </div>
        <div v-if="isStarted && !isRating" v-for="hero in heroes1" class="startBlock">
            <div class="hero" @click="choose(hero.id)">
                <div class="heroName">{{hero.fullname}}</div>
                <div class="heroImg">
                    <img v-bind:src="hero.photoLink"/>
                </div>
            </div>
        </div>
        <div>
        <div class="ratingList" v-if="isRating" v-for="hero in heroes" >
          <div id="elements">
          <div class="text1">#{{c++-500}}
          </div>
            <img width="80" height="80" v-bind:src="hero.photoLink"/>
            <div class="text">{{ hero.fullname}}</div>
            <div class="text">{{Math.ceil(hero.rating)}}</div>
            
          </div>
          <div class="divisor"></div>
        </div>
    </div>
    </div>
</template>

<style>


#mainPage{
  margin-left: auto;
  margin-right: auto;
  color: blue;
  width: 90%;
  margin-top: 10px;
  font-weight: normal;
}
.divisor{
    height: 3px;
    background-color: gold;
}
.text1{
  margin-left: 30px;
  width: 200px;
  color: #5CB85C;
  font-size: 35px;
}
.text{
  margin-left: 30px;
  width: 200px;
  color: #007bff;
  font-size: 20px;
}
#elements{
  display: flex;
  margin-left: 20px;
}
.startBlock1{
    margin-left: auto;
    margin-right: auto;
    text-align: center;
}
.startBlock{
    margin-left: 70px;
    display:inline-block;
    justify-content: space-between;
    font-size: 30px;
}
.hero{
    margin-left: 100px;
    border: 5px solid #007bff;
    color: #007bff;
}
.hero:hover{
    border: 5px solid gold;
    color: gold;
}
.heroImg img{
    width: 320px;
    height: 320px;
}
.header{
    height: 55px;
    width:80%;
}
.search{
    margin-left: 50px;
    width: 230px;
    height: 40px;
    border-radius: 10px;
    background: transparent;
    color: gold;
}
.searchbtn{
    width: 120px;
    margin-left: 10px;
    height:40px;
    background-color: gold;
    color: crimson;
    border: 2px solid gold;
    border-radius: 10px; 

    transition: 0.2s;
}
.searchbtn:hover{
    background-color: crimson;
    color: gold;
}
.rating{
    width: 200px;
    margin-left:300px;
    height:40px;
    background-color: #5CB85C;
    color: #007bff;
    border: 2px solid #5CB85C;
    border-radius: 10px; 
    transition: 0.2s;
}
.rating:hover{
    color: gold;
}
.startbtn{
    width: 400px;
    margin-top: 120px;
    font-size: 36px;
    height:120px;
    background-color: #5CB85C;
    color: gold;
    border: 2px solid #5CB85C;
    border-radius: 20px; 
    transition: 0.2s;
}
.startbtn:hover{
    background: transparent;
}
.startBlock{
    text-align: center;
}
</style>
