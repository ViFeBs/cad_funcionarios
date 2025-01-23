import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import  Button  from '../../components/Button';
import style from '../../styles/login/Login.module.css';

function Login() {
  const [login, setLogin] = useState('');
  const [senha, setSenha] = useState('');
  const [error, setError] = useState(false);
  const navigate = useNavigate();


  //função que ativa quando usuario clica em entrar
  const handleSubmit = async (e) => {
    e.preventDefault();

    const loginData = {
      login,
      senha,
    };

    try {
      const response = await fetch(`http://localhost:5000/api/Login/authenticate`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(loginData),
      });

      if (response.ok) {
        const data = await response.json();
        const user = {
          loginId : data.loginId
        }
        // Armazene o token de autenticação
        localStorage.setItem('token', user);
        // Redirecione para a página principal ou painel
        navigate('/home');
      }
      else {
        setError(true)
      }
    } catch (error) {
      console.error('Erro ao fazer login:', error);
      setError(true);
    }
  };
  return (
    <div>
      <div className={style.body}>
        <div className={style.container}>
            <div className={style.box}>
                
                    <form className={style.transferform} onSubmit={handleSubmit}>
                     {/*caso tenha alguma credencial errada a mensagem será exibida*/} 
                    {error && <p className={style.errorMessage}>Credenciais incorretas. Tente novamente.</p>}
                        <label htmlFor="login">Documento:</label>
                        <input
                            type="text"
                            id="login"
                            name="login"
                            value={login}
                            onChange={(e) => setLogin(e.target.value)}
                            className={error ? style.errorInput : ''}
                        />

                        <label htmlFor="login">Senha:</label>
                        <input
                            type="password"
                            id="senha"
                            name="senha"
                            value={senha}
                            onChange={(e) => setSenha(e.target.value)}
                            className={error ? style.errorInput : ''}
                        />
                        <Button color="base" action="submit" text="Entrar" />
                    </form>  
            </div>
        </div>
      </div>
    </div>
  );
}

export default Login;
