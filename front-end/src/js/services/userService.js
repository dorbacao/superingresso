import toastr from "./../components/toast";
import answer from "./../my-components/answer";

export class userService {

    constructor() {

    }

    async changeUserAsync(user) {

        const url = `${import.meta.env.VITE_APP_URL}/user/${user.id}`;
        
        var body = JSON.stringify(user);

        var response = await fetch(url, this.defaultHeader({
            method: 'PUT',
            body: body
        }));

        answer.fromResponse(await response.json());

        console.log(response);

        if (!response.ok) {
            toastr.error(response.statusText);
            return;
        }

        var result = await response.text();

        toastr.success(result);
    }

    getToken(){
        return JSON.parse(localStorage.getItem('token')).token;
    }
    defaultHeader(otherHeaders){
        var defaultHeader = {headers:{
            'Authorization': `Bearer ${this.getToken()}`,
            'Content-Type': 'application/json'
        }};

        var concat = {...defaultHeader, ...otherHeaders};

        return concat;
    }
    async getUserById(id) {

        const url = `${import.meta.env.VITE_APP_URL}/user/${id}`;

        var response = await fetch(url, this.defaultHeader());

        var userDetail = await response.json();

        return userDetail;
    }

    async changePassword(id, senha, confirmaSenha) {

        const url = `${import.meta.env.VITE_APP_URL}/user/${id}/password`;

        var response = await fetch(url, {
            method: 'PATCH',
            body: JSON.stringify({ 'senha': senha, 'confirmaSenha': confirmaSenha }),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        var userDetail = await response.json();

        return userDetail;
    }

    async getAllAsync() {

        const url = `${import.meta.env.VITE_APP_URL}/user?pageindex=1&pagesize=10`;

        var response = await fetch(url, this.defaultHeader());
        
        var content = await response.json();

        answer.fromResponse(content);

        console.log(content);

        return content.value;
    }

}