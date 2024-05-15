import React, { useState } from "react";

const ContactForm = () => {
  const [email, setEmail] = useState("");
  const [message, setMessage] = useState("");


  const handleSubmit = () => {
    return;
  }


  return (
    <div className="contact">
      <div className="form-group">
        <label htmlFor="email">Your email</label>
        <input
          type="email"
          className="form-control"
          placeholder="name@example.com"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </div>
      <div className="form-group">
        <label htmlFor="message">Your Message</label>
        <textarea
          className="form-control"
          id="message"
          rows="3"
          value={message}
          onChange={(e) => setMessage(e.target.value)}
        />
      </div>
      <div className="d-flex justify-content-end">
        <button
          type="button"
          onClick={handleSubmit}
          className="btn btn-outline-dark"
        >
          Send message
        </button>
      </div>
    </div>
  );
};

export default ContactForm;
