


<template>
  <div id="adminPage">
    <div>
      <h1 class="header">Добавить героя:</h1>
    </div>
     <div class="incorrectLoginOrPassword">
     </div>
    <form @submit.prevent="addHero">
      
      <div>
        <div class="label" for="fullname">
          <p  >Полное имя персонажа:</p>
        </div>
        <input name="fullname" v-model="fullname" placeholder="">
      </div>
      <div>
        <div for="photoLink" class="label">
         <p >Ссылка на фото героя:</p>
        </div>
        <input name="photoLink" v-model="photoLink" placeholder="" type="text">
      </div>
      <div>
        <div for="rating" class="label">
         <p >Рейтинг героя</p>
        </div>
        <input name="rating" v-model="rating" placeholder="" type="number">
      </div>
        
      <input class="btn" type="submit" value="Готово">
    </form>
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
      rating: 0
    };
  }, 
  updated(){
   
  },
  methods: {
    async addHero() {
      
        const { id,photoLink, fullname, rating} = this;
        const res = await fetch(
          "https://localhost:5001/api/Auth",
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json"
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
  width: 400px;
  background-color: white;
  margin-left: auto;
  margin-right: auto;
  margin-top: 120px;
  border-radius: 10px;
}
.header{
  text-align: center;
  color: #3CAEA3;
}
form {
    margin-left: auto;
    margin-right: auto;
    width: 60%;
    text-align: center;
    padding: 20px;
    padding-left: 0px;
    border-radius: 10px;

}

.label {
    height: 20px;
    font-size: 12px;
    text-align:left;
    color: red;
    text-font: sans-serif;
}
.incorrectLoginOrPassword {

    height: 25px;
    font-size: 18px;
    text-align:center;
    color: red;
    text-font: sans-serif;
}
.optional{
  color: gray;
}

input, select {
    margin-top: 5px;
    display: block;
    padding: 10px 6px;
    width: 100%;
    box-sizing: bordre-box;
    border: 1px solid #3CAEA3;
    border-radius: 4px;

}

input[type="checkbox"] {
    width:16px;
    margin: 0 10px 0;
    position: relative;
    top: 2px;
}

.pill {
    display: inline-block;
    margin: 20px 10px 0 0 ;
    padding: 6px 12px;
    border-radius: 20px;
    font-size: 12px;
    cursor: pointer;
}

.btn {
    width: 150px;
    color: white;
    background-color: #007bff;
    border-radius: 10px;
    margin: auto;
    margin-top: 5px;
}



.error {
    color: #ff0000;
    margin-top: 10px;
    font-size: 0.8em;
    font-weight: bold;
}
</style>



Hope this is helpful 🙂
Post navigation
← Previous Post
Next Post →
