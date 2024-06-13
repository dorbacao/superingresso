import toastr from "./../components/toast";

export class authService {

    constructor() {

    }

    async createAndLogin(user) {

        const url = `${import.meta.env.VITE_APP_URL}/auth`;

        var response = await fetch(url, {
            method: 'POST',
            body: JSON.stringify(user),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        console.log(response);
        if(!response.ok){
            toastr.error(await response.text());    
            console.log(response.statusText);
            return;
        }

        var token = await response.json();

        console.log(token);

        localStorage.setItem("login", JSON.stringify(token));

        toastr.success("Usuário autenticado com sucesso. Iniciando a sessão!");

        setTimeout(()=>{
            window.location = "index.html";
        }, 1000);

        return token;
    }

    async login(login, senha) {

        const url = `${import.meta.env.VITE_APP_URL}/user/login`;

        var response = await fetch(url, {
            method: 'POST',
            body: JSON.stringify({ 'token': login, 'senha': senha }),
            headers: {
                //'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json'
            }
        });

        console.log(response);
        if(!response.ok){
            toastr.error("Login e/ou senha inválidos");    
            console.log(response.statusText);
            return;
        }

        var token = await response.json();

        localStorage.setItem("login", JSON.stringify(token));

        toastr.success("Usuário autenticado com sucesso");

        setTimeout(()=>{
            window.location = "index.html";
        }, 200);

        return token;
    }

}