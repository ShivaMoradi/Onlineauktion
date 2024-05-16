import { useContext, useState } from "react";
import { AuthContext } from "../context/AuthContext";
import { GlobalContext } from "../context/GlobalContext";
import { Link } from "react-router-dom";
import AuctionCard from "./AuctionCard";
import AuctionForm from "./Auctionform";

function UserDashboard() {
    const { carItem, auction } = useContext(GlobalContext);
    const { logout, user } = useContext(AuthContext);

    const userAuctions = auction.filter((auction) => auction.userId === user.id);
    const userCars = userAuctions.map((auction) =>
        carItem.find((car) => car.id === auction.carId)
    );

    const [showForm, setShowForm] = useState(false);
    const [auctionForm, setAuctionForm] = useState(null);

    const handleShowForm = () => {
        setAuctionForm(null);
        setShowForm(true);
    };

    const handleAuctionSubmit = (auctionFormData) => {
        if (auctionForm) {
            console.log(auctionFormData, "Was updated in the Database.");
            alert("Auction updated successfully.");
        } else {
            console.log(auctionFormData, "Was created in the Database.");
            alert("Auction created successfully.");
        }
        setShowForm(false);
    };

    const handleLogout = () => {
        alert("Goodbye");
        logout();
    };

    const handleEditAuction = (car) => {
        return () => {
            const auctionToEdit = auction.find((a) => a.carId === car.id);
            console.log("Auction to edit: (should be imageURL here): ", auctionToEdit, car);
            setAuctionForm({ ...auctionToEdit, ...car });
            setShowForm(true);
        };
    };

    const handleCloseForm = () => {
        setShowForm(false);
        setAuctionForm(null);
    };

    return (
        <>
            <h1>User Dashboard</h1>
            <p>Welcome {user.name} </p>

            <br></br>
            <div>
                {showForm ? (
                    <AuctionForm auction={auctionForm} onSubmit={handleAuctionSubmit} closeForm={handleCloseForm} />
                ) : (
                    <button data-test="create-auction-btn" className="btn btn-primary" onClick={handleShowForm}>
                        Create new Auction
                    </button>
                )}
            </div>

            <div>
                <br></br>
                <button data-test="logout-button" onClick={handleLogout} className="btn btn-primary">
                    Logout
                </button>
            </div>

            {userCars.map((car) => (
                <div className="py-3" key={car?.id}>
                    <div className="row row-cols-1 row-cols-md-3 g-3" key={car?.id}>
                        <div className="col-12 col-sm-6 col-md-4" key={car?.id}>
                            {car && (
                                <Link to={`/cars/${car.id}`} key={car.id} style={{ textDecoration: "none" }}>
                                    <AuctionCard key={car.id} item={car} />
                                </Link>
                            )}
                            <button onClick={handleEditAuction(car)}>Edit</button>
                        </div>
                    </div>
                </div>
            ))}
        </>
    );
}

export default UserDashboard;