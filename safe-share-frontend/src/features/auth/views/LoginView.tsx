import {type SubmitEvent} from "react";
import { User, Lock } from "lucide-react";
import {Link} from "react-router-dom";
import AuthCard from "../componenets/AuthCard.tsx";

export default function LoginView() {
    const handleSubmit = (e: SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log("Login clicked!")
    }

    return  (
        <AuthCard title={"Log in"}>
            <form onSubmit={handleSubmit} className="flex flex-col h-full space-y-6 mt-5">
                <div>
                    <label>User name</label>
                    <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.1)] h-10 rounded-lg items-center pl-2 gap-4">
                        <User />
                        <input type="text" placeholder="Your login" className="w-full"/>
                    </div>
                </div>
                <div>
                    <label>Password</label>
                    <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.1)] h-10 rounded-lg items-center pl-2 gap-4">
                        <Lock />
                        <input type="password" placeholder="Your password" className="w-full"/>
                    </div>
                </div>
                <div>
                    <button type="submit" className="w-full rounded-lg bg-[rgba(227,197,215)] text-black cursor-pointer h-10 mt-5 font-bold hover:bg-[rgba(227,197,215,0.8)] hover:text-black/60 ">Enter</button>
                </div>
                <div className="flex justify-center">
                    <p>Don't have an account? <Link to="/register" className="font-bold hover:underline">Register</Link></p>
                </div>
            </form>
        </AuthCard>
    )
}