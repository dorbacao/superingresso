import toastr from 'toastr';

export class userService {

    constructor() {

    }

    async changeUserAsync(user) {

        console.log('user');
        console.log(user);
        const url = `${import.meta.env.VITE_APP_URL}/user/${user.id}`;
        
        var body = JSON.stringify(user);


        var response = fetch(url, {
            method: 'PUT',
            body: body,
            headers: {
                'Content-Type': 'application/json',
            }
        }).then(r=>{

            console.log('r')
            console.log(r)

        })
        .catch((erro, x2)=>{
            console.log(erro)
            console.log(x2)
        })
        
        ;

        var response = await fetch(url, {
            method: 'PUT',
            body: body,
            headers: {
                'Content-Type': 'application/json',
            }
        });

        console.log(response);
        if (!response.ok) {
            toastr.error(response.statusText);
            return;
        }

        var result = await response.text();

        toastr.success(result);


    }
    async getUserById(id) {

        const url = `${import.meta.env.VITE_APP_URL}/user/${id}`;

        var response = await fetch(url);

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

        const url = `${import.meta.env.VITE_APP_URL}/user`;

        var response = await fetch(url);

        console.log(response);
        if (!response.ok) {
            toastr.error("Login e/ou senha inválidos");
            console.log(response.statusText);
            return;
        }

        var userList = await response.json();

        return userList;
    }

}