


<template>
  <div id="authPage">
    <div>
      <h1 class="header">Вход</h1>
    </div>
     <div class="incorrectLoginOrPassword">
      <p v-if="responeFromServer=='Unauthorized'">Неправильный логин или пароль</p>
     </div>
    <form @submit.prevent="auth">
      
      <div>
        <div class="label" for="login">
          <p v-if="errors.login && (isLoginTouched || wasAttemptToSend) ">{{errors["login"]}}</p>
          <p v-else>*</p>
        </div>
        <input name="login" v-model="login" placeholder="Логин">
      </div>
      <div>
        <div for="password" class="label">
          <p v-if="errors.password && wasAttemptToSend">{{errors["password"]}}</p>
          <p v-else-if="errors.notEnoughSymbols && (isPasswordTouched || wasAttemptToSend)">{{errors["notEnoughSymbols"]}}</p>
          <p v-else>*</p>
        </div>
        <input name="password" v-model="password" placeholder="Пароль" type="password">
      </div>
        
      <input class="btn" type="submit" value="Готово">
    </form>
  </div>
</template>
<script>

export default {
  name: "AuthPage",
  data() {
    return {
      errors: {
        "login":"",
        "password":"",
        "isEmpty":true,

      },
      isReady: false,
      wasAttemptToSend:false,
      isPasswordTouched:false,
      isLoginTouched:false,
      responeFromServer:"",
      wasRequest:false,
      Id: 0,
      login: "",
      password: "",
      loginModel: null
    };
  }, 
  updated(){
    if(this.isLoginTouched && this.isPasswordTouched){
      this.isReady = true
    }
    if(!this.errors["login"] && !this.errors["password"]){
          this.errors["isEmpty"] = true
    }
    if(!this.login){
      this.errors["login"] = "Логин должен быть заполнен"
      this.errors.isEmpty = false
    }else{
      this.errors["login"] = ""
      this.isLoginTouched = true
    }
    
    
    if(!this.password){
      this.errors["password"] = "Пароль должен быть заполнен"
      this.errors.isEmpty = false
    }else{
      this.errors["password"] = ""
      this.isPasswordTouched = true
    }
   
  },
  methods: {
    async auth() {
      this.wasAttemptToSend = true;
      if(this.errors.isEmpty && this.isReady){
        this.wasRequest = true
        const { Id,login, password} = this;
        const res = await fetch(
          "https://localhost:5001/api/Auth",
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json"
            },
            body: JSON.stringify({
              Id,
              login,
              password,
            })
          }
        ).then(response=>{
          this.responeFromServer = response.statusText

            return response.json()
          }).then(data=>{
            if(this.responeFromServer != "Unauthorized"){
              localStorage.setItem( 'tokens', JSON.stringify(data) );
              window.location.href = 'http://localhost:8080/admin';
            }
        });
        let data = res.json()
        console.log(data)
        
      }
    }
    
  }
};
</script>

<style scoped> 
#authPage{
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
