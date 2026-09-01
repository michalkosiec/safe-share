export default function VaultView() {
    return (
        <main className="p-6 md:p-8 h-full w-full overflow-y-auto">
            <div className="grid grid-cols-[300px_1fr] gap-6 h-full">
                <aside className="bg-black/85 rounded-2xl border border-white/10 p-5 shadow-lg backdrop-blur-sm">
                    <h2 className="text-2xl font-bold text-white mt-2">Navbar</h2>
                </aside>
                <div className="grid grid-cols-12 gap-4">

                    <header className="col-span-12 mb-2">
                        <h1 className="text-3xl font-bold text-white">Your Safe</h1>
                        <p className="text-gray-100 mt-1">Manage your encrypted files and access keys</p>
                    </header>

                    <div className="col-span-12 md:col-span-4 bg-black/85 rounded-2xl border border-white/10 p-5 shadow-lg backdrop-blur-sm">
                        <h2 className="text-sm font-medium text-gray-100">All of the files</h2>
                        <p className="text-3xl font-bold text-white mt-2">0</p>
                    </div>
                    <div className="col-span-12 md:col-span-4 bg-black/85 rounded-2xl border border-white/10 p-5 shadow-lg backdrop-blur-sm">
                        <h2 className="text-sm font-medium text-gray-100">Used storage</h2>
                        <p className="text-3xl font-bold text-white mt-2">0 B</p>
                    </div>
                    <div className="col-span-12 md:col-span-4 bg-black/85 rounded-2xl border border-white/10 p-5 shadow-lg backdrop-blur-sm">
                        <h2 className="text-sm font-medium text-gray-100">Shared keys</h2>
                        <p className="text-3xl font-bold text-white mt-2">0</p>
                    </div>

                    <section className="col-span-12 lg:col-span-8 bg-black/85 rounded-2xl border border-white/10 p-6 shadow-lg min-h-100 backdrop-blur-sm">
                        <div className="flex justify-between items-center mb-6">
                            <h2 className="text-xl font-semibold text-white">Your files</h2>
                        </div>

                        <div className="flex flex-col items-center justify-center h-64 text-gray-100 border-2 border-dashed border-white/10 rounded-xl">
                            <p>No files yet. Upload something to get started</p>
                        </div>
                    </section>

                    <aside className="col-span-12 lg:col-span-4 bg-black/85 rounded-2xl border border-white/10 p-6 shadow-lg backdrop-blur-sm">
                        <h2 className="text-xl font-semibold text-white mb-6">Last activity</h2>

                        <div className="space-y-4">
                            <p className="text-sm text-gray-100 italic">No new events</p>
                        </div>
                    </aside>

                </div>
            </div>
        </main>
    );
}