<script >
export default {
  data() {
    return {
        isStarted: false,
        isRating: false,
        heroes:[],
        searchedHeroes:[],
        c:1,
        counter: 0,
        searchStr:"",
        isSearched:false,
        heroes1:{},
    }
  },
  updated(){
      this.c = 1
  },
  methods:{
      async search(){
          this.isSearched = true
          const searchStrToServer = this.searchStr
          await fetch(
          `https://localhost:5001/api/MainPage/search`,
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json"
            },
            body: JSON.stringify({
                name: searchStrToServer
            })
          }
        ).then(res=>res.json()).then(res=>this.searchedHeroes = res)
      },
      back(){

        this.isRating = false;
        this.isSearched = false
      },
      restart(){
          this.isStarted = false
          this.counter = 0
      },
      choose(id){
          if(this.counter > 11)return
          this.counter++
          if(this.counter <12){
              
                console.log(this.counter)
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
            }else{
                if(this.heroes1[0].id == id){
                    this.heroes1.pop()
                }else{
                    this.heroes1.shift()
                }
            }
          
      },
      startGame(){
          this.isRating= false
          this.isStarted = true
          fetch("https://localhost:5001/api/MainPage/vs")
            .then( response => response.json() )
            .then( response => {
                this.heroes1 = response;
            } );
      },
      async getRating(){
        this.c = 1
        this.isSearched = false
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
            <input v-model="searchStr" class="search" type="text" placeholder="Поиск..."/>
            <button class="searchbtn" @click="search()" >Искать</button>
            <button v-if="!isRating && !isSearched" @click="getRating()" class="rating" >Рейтинг Героев</button>
            <button v-if="isRating || isSearched" @click="back()" class="rating" >Главная</button>
            
        </div>
        <div v-if="!isStarted && !isRating && !isSearched" class="startBlock1">
            <button @click="startGame()" class="startbtn">НАЧАТЬ!</button>
        </div>
        
        <div v-if="isStarted && !isRating && !isSearched" v-for="hero in heroes1" class="startBlock">
            <div class="hero" @click="choose(hero.id)">
                <div class="heroName">{{hero.fullname}}</div>
                <div class="heroImg">
                    <img v-bind:src="hero.photoLink"/>
                </div>
            </div>
        </div>
        <div class="winner" v-if="counter == 12 && !isRating">Победитель!</div>
        <button v-if="counter == 12 && !isRating && !isSearched" @click="restart()" class="restart" >Рестарт</button>
        <div>
        <div class="ratingList" v-if="isRating" v-for="hero in heroes" >
          <div id="elements">
            <div class="text1">#{{heroes.indexOf(hero)+1}}</div>
            <img width="80" height="80" v-bind:src="hero.photoLink"/>
            <div class="text">{{ hero.fullname}}</div>
            <div class="text">{{Math.ceil(hero.rating)}}</div>
            
          </div>
          <div class="divisor"></div>
        </div>
    </div>
        <div class="searchedHeroes" v-if="isSearched" v-for="hero in searchedHeroes" >
          <div id="elements">
            <img width="80" height="80" v-bind:src="hero.photoLink"/>
            <div class="text">{{ hero.fullname}}</div>
            <div class="text">{{Math.ceil(hero.rating)}}</div>
            
          </div>
          <div class="divisor"></div>
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
.winner{
    text-align:center;
    transition:1s;
    font-size: 60px;
    color: #5CB85C;
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
    
    text-align: center;
}
.startBlock{
    transition:0.5s;
    display:inline-flex;
    margin-left: 170px;
    text-align: center;
    font-size: 30px;
}
.hero{
    transition:.5s;
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
.restart{
    width: 200px;
    margin-left:450px;
    height:40px;
    background-color: #5CB85C;
    color: #007bff;
    border: 2px solid #5CB85C;
    border-radius: 10px; 
    transition: 0.2s;
    text-align:center;
}
.restart:hover{
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
