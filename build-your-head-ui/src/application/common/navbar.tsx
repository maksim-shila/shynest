import React from "react";
import { Nav, Navbar as ReactstrapNavbar, NavbarBrand, NavItem, NavLink, UncontrolledDropdown, DropdownToggle, Row, Col, DropdownMenu, DropdownItem } from "reactstrap";
import { GlobalContext } from "../context/global-context";

export const NavBar: React.FC = () => {

    const { $user } = React.useContext(GlobalContext);

    const handleLogoutClick = () => {
        console.log("IN PROGRESS");
    }

    return (
        <ReactstrapNavbar
            full={true}
            fixed="top"
            color="light"
            className="px-3"
        >
            <NavbarBrand href="/">Build Your Head</NavbarBrand>
            <Nav className="me-auto" color="light" navbar>
                <Row>
                    <Col>
                        <NavItem>
                            <NavLink href="/products">Products</NavLink>
                        </NavItem>
                    </Col>
                    <Col>
                        <NavItem>
                            <NavLink href="/recipes">Recipes</NavLink>
                        </NavItem>
                    </Col>
                </Row>
            </Nav>
            {$user()
                ? <UncontrolledDropdown inNavbar>
                    <DropdownToggle nav caret>
                        {`Hi, ${$user()?.name}`}
                    </DropdownToggle>
                    <DropdownMenu right>
                        <DropdownItem onClick={handleLogoutClick}>Logout</DropdownItem>
                    </DropdownMenu>
                </UncontrolledDropdown>
                : <NavLink href="/login">Log In</NavLink>
            }
        </ReactstrapNavbar>
    );
}