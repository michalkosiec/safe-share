export default function Register() {
    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        console.log("Register clicked!")
    }
    return <div>
        <form onSubmit={handleSubmit}>
            <button type="submit">Register</button>
        </form>
    </div>
}