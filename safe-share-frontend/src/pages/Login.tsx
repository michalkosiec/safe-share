export default function Login() {
    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        console.log("Login clicked!")
    }
    return <div>
        <form onSubmit={handleSubmit}>
            <button type="submit">Login</button>
        </form>
    </div>
}